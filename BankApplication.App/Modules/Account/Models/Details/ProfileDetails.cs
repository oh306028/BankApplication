namespace BankApplication.App.Modules.Account.Models.Details
{
    public class ProfileDetails
    {
        public string Login { get; set; }
        public bool IsDoubleAuthenticated { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PESEL { get; set; }
        public DateTime BirthDate { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Number { get; set; }
    }
}
