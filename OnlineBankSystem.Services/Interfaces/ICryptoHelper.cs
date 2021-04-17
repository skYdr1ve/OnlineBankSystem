using System.Threading.Tasks;

namespace OnlineBankSystem.Services.Interfaces
{
    public interface ICryptoHelper
    {
        public string Hash(string value);
    }
}
