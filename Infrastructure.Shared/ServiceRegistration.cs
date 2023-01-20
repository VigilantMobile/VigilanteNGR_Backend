using Application.Interfaces;
using Domain.Settings;
using Infrastructure.Shared.LoggerClass;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            try
            {
                services.Configure<MailSettings>(_config.GetSection("MailSettings"));
                services.Configure<SMSSettings>(_config.GetSection("SMSSettings")); 
                services.AddTransient<IDateTimeService, DateTimeService>();
                services.AddTransient<IEmailService, EmailService>();
                services.AddTransient<ISMSService, SMSService>();
                services.AddScoped<IMailgunEmailService, MailgunEmailService>();
                services.AddTransient<IRandomNumberGeneratorInterface, RandomNumberGeneratorService>();
                services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<ApplicationLogs>>());
                services.AddMemoryCache();
                //string MailChimpAPIKey = _config["MailSettings:MailChimpAPIKey"];
                //var api = new MandrillApi(MailChimpAPIKey);
                //services.AddSingleton<IMandrillMessagesApi, MandrillEmailService>();

            }
            catch(Exception e)
            { 
                Console.WriteLine(e.ToString());    
            }
          
        }
    }
}
