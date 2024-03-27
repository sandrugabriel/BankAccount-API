namespace BankAccountAPI.Dto
{
    public class UpdateBankRequest
    {

        public int? Balance { get; set; }

        public string? Type { get; set; }

        public string? Name { get; set; }
    }
}
