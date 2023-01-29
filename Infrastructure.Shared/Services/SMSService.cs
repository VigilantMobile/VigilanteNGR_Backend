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
using Infrastructure.Shared.Services.Notification.SMSHelperClasses;
using Newtonsoft.Json;

namespace Infrastructure.Shared.Services
{
    public class SMSService : ISMSService
    {

        public SMSSettings _smsSettings { get; }
        public ILogger<SMSService> _logger { get; }

        public SMSService(IOptions<SMSSettings> smssettings, ILogger<SMSService> logger)
        {
            _smsSettings = smssettings.Value;
            _logger = logger;
        }

        public async Task<RestResponse> SendSMSAsync(SMSRequest smsRequest)
        {
            try
            {

                RestClientOptions opts = new RestClientOptions();
                opts.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                opts.BaseUrl = new Uri($"{_smsSettings.D7APISMSBase}");

                using (RestClient client = new RestClient(opts))
                {
                    var request = new RestRequest("messages/v1/send", Method.Post);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("Authorization", $"Bearer {_smsSettings.D7SMSAPIToken}");
                    request.AddParameter("application/json", JsonConvert.SerializeObject(smsRequest), ParameterType.RequestBody);
                    RestResponse resp = await client.ExecuteAsync(request);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

         public async Task<RestResponse> SendOTPRequest(GenerateOTPrequest smsRequest)
         {
            try
            {

                RestClientOptions opts = new RestClientOptions();
                opts.RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                opts.BaseUrl = new Uri($"{_smsSettings.D7APISMSBase}");

                using (RestClient client = new RestClient(opts))
                {
                    var request = new RestRequest("verify/v1/otp/send-otp",Method.Post);
                    client.Authenticator = new HttpBasicAuthenticator(_smsSettings.ClientId, _smsSettings.ClientSecret);
                    request.AddHeader("X-RapidAPI-Key", _smsSettings.D7SMSAPIKey);
                    request.AddHeader("X-RapidAPI-Host", _smsSettings.D7APISMSBase);
                    request.AddHeader("content-type", "application/json");
                    request.AddHeader("accept", "application/json");
                    request.AddParameter("application/json", JsonConvert.SerializeObject(smsRequest), ParameterType.RequestBody);
                    RestResponse resp = await client.ExecuteAsync(request);
                    return resp;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }
    }
}
