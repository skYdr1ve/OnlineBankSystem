using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ISmsService
    {
        public Task<MessageResource> SendAsync(string destination, string message);
    }
}
