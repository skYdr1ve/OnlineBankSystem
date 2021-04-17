using System.Security.Cryptography;
using System.Text;
using OnlineBankSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace OnlineBankSystem.Services.Services
{
    public class CryptoHelper : ICryptoHelper
    {
        public string Hash(string value)
        {
            var result = "";

            using (var sha512 = new SHA512Managed())
            {
                var hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                result = sb.ToString().ToLower();
            }

            return result;
        }
    }
}
