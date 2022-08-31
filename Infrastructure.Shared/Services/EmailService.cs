using Application.DTOs.Email;
using Application.Exceptions;
using Domain.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
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

        public async Task SendAsync(EmailRequest request)
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
