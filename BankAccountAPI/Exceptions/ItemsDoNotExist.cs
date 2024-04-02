namespace BankAccountAPI.Exceptions
{
    public class ItemsDoNotExist :Exception
    {
        public ItemsDoNotExist(string? message):base(message) { }
    }
}
