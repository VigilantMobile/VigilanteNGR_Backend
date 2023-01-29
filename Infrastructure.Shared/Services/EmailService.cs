using Application.DTOs.Email;
using Application.Exceptions;
using Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendSMTPAsync(EmailRequest request)
        {
            try
            {

                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\GenericTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("[user]", request.Username).Replace("[heading]", request.Subject).Replace("[paragraph1]", request.BodyParagraph1);

                if (string.IsNullOrEmpty(request.BodyParagraph2))
                {
                    MailText.Replace("[paragraph2]", request.BodyParagraph2);
                }
                else
                {
                    MailText.Replace("[paragraph2]", "");
                }

                // create message
                var email = new MimeMessage();
                email.Sender = new MailboxAddress(_mailSettings.DisplayName, request.From ?? _mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();

                if (request.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in request.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                //builder.HtmlBody = request.Body;
                //email.Body = builder.ToMessageBody();

                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        public async Task SendWelcomeEmailAsync(EmailRequest request)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\Services\\Notification\\EmailHelperClasses\\EmailTemplates\\CustomerWelcomeTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();
                MailText = MailText.Replace("[heading]", request.Heading).Replace("[email]", request.To);
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = $"Welcome {request.Username}";


                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.EmailFrom, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }

        }

        public async Task SendStaffVerificationEmailAsync(EmailRequest request)
        {

            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\Services\\Notification\\EmailHelperClasses\\EmailTemplates\\GenericTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();


                MailText = MailText.Replace("[username]", request.Username).Replace("[heading]", request.Heading).Replace("[paragraph1]", request.BodyParagraph1);

                if (request.BodyParagraph2 != null)
                {
                    MailText.Replace("[paragraph2]", request.BodyParagraph2);
                }
                else
                {
                    MailText.Replace("[paragraph2]", "");
                }

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = MailText;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.EmailFrom, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }

        }

        public async Task<RestResponse> SendAsync(EmailRequest emailRequest)
        {
            try
            {
                string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\UserRegWelcomeTemplate.html";
                StreamReader str = new StreamReader(FilePath);
                string MailText = str.ReadToEnd();
                str.Close();

                MailText = MailText.Replace("[user]", emailRequest.Username).Replace("[heading]", emailRequest.Subject).Replace("[paragraph1]", emailRequest.BodyParagraph1);


                if (!string.IsNullOrEmpty(emailRequest.ButtonUrl))
                {
                    MailText = MailText.Replace("[buttonlabel]", emailRequest.ButtonLabel);
                    MailText = MailText.Replace("[buttonurl]", emailRequest.ButtonUrl);
                }

                if (string.IsNullOrEmpty(emailRequest.BodyParagraph1))
                {
                    MailText = MailText.Replace("[paragraph1]", ""); //will do nothing if paragraph does not exist
                }
                else
                {
                    MailText = MailText.Replace("[paragraph1]", emailRequest.BodyParagraph2);
                }
                if (string.IsNullOrEmpty(emailRequest.BodyParagraph2))
                {
                    MailText = MailText.Replace("[paragraph2]", ""); //will do nothing if paragraph does not exist
                }
                else
                {
                    MailText = MailText.Replace("[paragraph2]", emailRequest.BodyParagraph2);
                }

                //

                RestClientOptions opts = new RestClientOptions();
                opts.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                opts.BaseUrl = new Uri($"{  _mailSettings.MailgunBaseUri}");

                using (RestClient client = new RestClient(opts))
                {
                    var request = new RestRequest();
                    client.Authenticator = new HttpBasicAuthenticator("api", _mailSettings.MailgunAPIKey);
                    request.AddParameter("domain", _mailSettings.MailgunEmailDomain, ParameterType.UrlSegment);
                    request.Resource = $"{_mailSettings.MailgunEmailDomain}/messages";
                    request.AddParameter("from", $"VGNGA Support <mailgun@{_mailSettings.MailgunEmailDomain}>");
                    request.AddParameter("to", emailRequest.To);
                    request.AddParameter("subject", emailRequest.Subject);
                    request.AddParameter("html", MailText);
                    request.Method = Method.Post;
                    return await client.ExecuteAsync(request);
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }


        //public async Task MandrillSendAsync(EmailRequest request)
        //{
        //    try
        //    {

        //        string FilePath = Directory.GetCurrentDirectory() + "\\EmailTemplates\\GenericTemplate.html";
        //        StreamReader str = new StreamReader(FilePath);
        //        string MailText = str.ReadToEnd();
        //        str.Close();

        //        MailText = MailText.Replace("[user]", request.Username).Replace("[heading]", request.Subject).Replace("[paragraph1]", request.BodyParagraph1);

        //        if (string.IsNullOrEmpty(request.BodyParagraph2))
        //        {
        //            MailText.Replace("[paragraph2]", request.BodyParagraph2);
        //        }
        //        else
        //        {
        //            MailText.Replace("[paragraph2]", "");
        //        }

        //        //
        //        var api = new MandrillApi(_mailSettings.MailChimpAPIKey);
        //        var message = new MandrillMessage();
        //        message.FromEmail = request.From;
        //        message.AddTo(request.To);
        //        message.Html = MailText;

        //        if (request.Attachments != null)
        //        {
        //            byte[] fileBytes;
        //            foreach (var file in request.Attachments)
        //            {
        //                if (file.Length > 0)
        //                {
        //                    using (var ms = new MemoryStream())
        //                    {
        //                        file.CopyTo(ms);
        //                        fileBytes = ms.ToArray();
        //                    }
        //                    message.Attachments.Add((MandrillAttachment)(file));
        //                }
        //            }
        //        }


        //        var result = await api.Messages.SendAsync(message);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        _logger.LogError(ex.Message, ex);
        //        throw new ApiException(ex.Message);
        //    }
        //}

    }
}
