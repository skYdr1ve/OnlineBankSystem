using System;
using System.Security.Cryptography;
using System.Text;
using OnlineBankSystem.Services.Interfaces;

namespace OnlineBankSystem.Services.Services
{
    public class AccountHelper : IAccountHelper
    {
        public string GenerateIban()
        {
            var bankAccount = GenerateAccountNumber().ToUpper();

            var bank =
                bankAccount.Substring(4, bankAccount.Length - 4) + bankAccount.Substring(0, 4);
            const int asciiShift = 55;
            var sb = new StringBuilder();
            foreach (var c in bank)
            {
                int v;

                if (char.IsLetter(c))
                {
                    v = c - asciiShift;
                }
                else
                {
                    v = int.Parse(c.ToString());
                }

                sb.Append(v);
            }
            var checkSumString = sb.ToString();
            int checksum = int.Parse(checkSumString.Substring(0, 1));
            for (int i = 1; i < checkSumString.Length; i++)
            {
                int v = int.Parse(checkSumString.Substring(i, 1));
                checksum *= 10;
                checksum += v;
                checksum %= 97;
            }

            checksum = 98 - checksum;

            if (checksum < 10)
            {
                bankAccount = "BY0" + checksum + bankAccount.Substring(3);
            }
            else
            {
                bankAccount = "BY" + checksum + bankAccount.Substring(4);
            }

            return bankAccount;
        }

        private string GenerateAccountNumber()
        {
            const string valid = "0123456789";
            var length = 16;
            var res = new StringBuilder();
            using (var rng = new RNGCryptoServiceProvider())
            {
                var uintBuffer = new byte[sizeof(uint)];

                while (length-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    var num = BitConverter.ToUInt32(uintBuffer, 0);
                    res.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return "BY00SKYB3014" + res.ToString();
        }

        public bool ValidateBankAccount(string bankAccount)
        {
            bankAccount = bankAccount.ToUpper();
            if (string.IsNullOrEmpty(bankAccount))
            {
                return false;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(bankAccount, "^[A-Z0-9]"))
            {
                bankAccount = bankAccount.Replace(" ", string.Empty);
                string bank =
                    bankAccount.Substring(4, bankAccount.Length - 4) + bankAccount.Substring(0, 4);
                int asciiShift = 55;
                StringBuilder sb = new StringBuilder();
                foreach (char c in bank)
                {
                    int v;
                    if (Char.IsLetter(c)) v = c - asciiShift;
                    else v = int.Parse(c.ToString());
                    sb.Append(v);
                }
                string checkSumString = sb.ToString();
                int checksum = int.Parse(checkSumString.Substring(0, 1));
                for (int i = 1; i < checkSumString.Length; i++)
                {
                    int v = int.Parse(checkSumString.Substring(i, 1));
                    checksum *= 10;
                    checksum += v;
                    checksum %= 97;
                }
                return checksum == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
