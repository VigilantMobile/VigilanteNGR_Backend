using Application.DTOs.Email;
using Application.Exceptions;
using Domain.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class MailgunEmailService : IMandrillEmailService
    {
        public MailSettings _mailSettings { get; }
        private readonly IUtilities _utilities;


        public ILogger<MailgunEmailService> _logger { get; }

        public MailgunEmailService(IOptions<MailSettings> mailSettings,ILogger<MailgunEmailService> logger, IUtilities utilities)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
            _utilities = utilities;
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
                opts.BaseUrl = new Uri($"{_mailSettings.MailgunBaseUri}");

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
    }
}
