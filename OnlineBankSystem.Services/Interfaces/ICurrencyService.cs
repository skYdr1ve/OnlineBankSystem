using System.Threading.Tasks;
using OnlineBankSystem.Services.Models;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ICurrencyService
    {
        public Task<ConvertModel> Convert(string from, string to, decimal amount);
    }
}
