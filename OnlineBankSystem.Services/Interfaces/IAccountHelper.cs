namespace OnlineBankSystem.Services.Interfaces
{
    public interface IAccountHelper
    {
        public string GenerateIban();
        public bool ValidateBankAccount(string bankAccount);
    }
}
