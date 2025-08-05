namespace BankApplication.App.Modules.Client.Models.Details
{
    public class BlockRequestDetails
    {
        public DateTime RequestDate { get; set; }
        public DateTime? ManagedDate { get; set; }
        public bool? IsAccepted { get; set; }
        public bool IsActive { get; set; }
        public string ClientName { get; set; }
        public string BankAccountNumber { get; set; }       
    }
}
