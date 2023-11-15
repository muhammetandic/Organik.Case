using System.Text;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Organik.Case.Application.Helpers;
using Organik.Case.Application.Interfaces.ExternalServices;
using Organik.Case.Application.Models;

namespace Organik.Case.Infrastructure.ExternalServices
{
    public class SmsService : ISmsService
    {
        private readonly AppSettings _appSettings;

        public SmsService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task SendSms(string code, string phone)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Organik-Auth", _appSettings.OrganikApiKey);
            client.BaseAddress = new Uri("https://api.organikhaberlesme.com");

            var message = $"""
				Sisteme giriş için kullanacağınız Kod: {code}
				""";

            var smsRequest = new OrganikSmsSendRequest(message, (uint)_appSettings.HeaderId, new string[] { phone });

            var serializeRequest = JsonConvert.SerializeObject(smsRequest);

            StringContent stringContent = new StringContent(serializeRequest, Encoding.UTF8, "application/json");
            
            await client.PostAsync("/sms/send", stringContent);
        }
    }
}