using AutoMapper;
using BankApplication.Data.Entities;

namespace BankApplication.App.Modules.Client.Models.Details
{
    public class ClientDetails
    {
        public Guid PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PESEL { get; set; }
        public bool IsActive { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Number { get; set; }

    }

    public class ClientDetailsProfile : Profile
    {
        public ClientDetailsProfile()
        {
            CreateMap<BankApplication.Data.Entities.Client, ClientDetails>();
        }
    }
}
