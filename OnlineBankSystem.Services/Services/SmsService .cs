using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using OnlineBankSystem.Services.Interfaces;
using OnlineBankSystem.Services.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace OnlineBankSystem.Services.Services
{
    public class SmsService : ISmsService
    {
        public TwilioOptions _config { get; private set; }

        public SmsService(IOptions<TwilioOptions> options)
        {
            _config = options.Value;
        }

        public Task SendAsync(string destination, string message)
        {
            TwilioClient.Init(_config.AccountSid, _config.AuthToken);

            var result = MessageResource.Create(
                new PhoneNumber(destination),
                from: new PhoneNumber(_config.FromNumber),
                body: message);

            Trace.TraceInformation(result.Status.ToString());

            return Task.FromResult(0);
        }
    }
}
