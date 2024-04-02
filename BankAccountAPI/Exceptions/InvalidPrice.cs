namespace BankAccountAPI.Exceptions
{
    public class InvalidPrice : Exception
    {
        public InvalidPrice(string? message):base(message) { }
    }
}
