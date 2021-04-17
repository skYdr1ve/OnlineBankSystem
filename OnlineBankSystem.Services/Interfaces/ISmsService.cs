using System.Threading.Tasks;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ISmsService
    {
        public Task SendAsync(string destination, string message);
    }
}
