namespace OnlineBankSystem.Services.Interfaces
{
    public interface ICardHelper
    {
        bool CheckLuhn(string creditCardNumber);

        string Generate16DigitNumber();

        string GeneratePinCode();

        string Generate3DigitSecurityCode();
    }
}
