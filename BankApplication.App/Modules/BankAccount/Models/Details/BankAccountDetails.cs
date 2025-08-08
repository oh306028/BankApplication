namespace BankApplication.App.Modules.BankAccount.Models.Details
{
    public class BankAccountDetails
    {
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }    
        public string Currency { get; set; }
        public decimal? InterestRate { get; set; }
        public string PublicId { get; set; }
        public bool IsBlocked { get; set; } 
    }
}
