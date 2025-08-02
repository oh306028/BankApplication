namespace BankApplication.App.Modules.BankAccount.Transfers.Models
{
    public class TransferDetails
    {
        public string SenderNumber { get; set; }  
        public string ReceiverNumber { get; set; }  
        public string Sender { get; set; }
        public string Receiver { get; set; }
            
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
