using AutoMapper;
using BankApplication.App.Modules.Client.Models.Details;
using FluentValidation;

namespace BankApplication.App.Modules.Client.Models.Create
{
    public class ClientForm
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PESEL { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Number { get; set; }

    }

    public class ClientFormValidator : AbstractValidator<ClientForm>
    {
        public ClientFormValidator()
        {
            RuleFor(f => f.FirstName).NotEmpty().MaximumLength(50).WithMessage("Imię jest wymagane");
            RuleFor(f => f.LastName).NotEmpty().MaximumLength(50).WithMessage("Nazwisko jest wymagane");
            RuleFor(f => f.Email).NotEmpty().MaximumLength(50).WithMessage("Email jest wymagany");
            RuleFor(f => f.Phone).NotEmpty().MaximumLength(20).WithMessage("Numer telefonu jest wymagany");
            RuleFor(f => f.PESEL).NotEmpty().MaximumLength(20).WithMessage("Pesel jest wymagany");
            RuleFor(f => f.Nationality).NotEmpty().MaximumLength(20).WithMessage("Narodowość jest wymagana");
            RuleFor(f => f.Country).NotEmpty().MaximumLength(30).WithMessage("Państwo jest wymagane");
            RuleFor(f => f.City).NotEmpty().MaximumLength(50).WithMessage("Miasto jest wymagane");
            RuleFor(f => f.PostalCode).NotEmpty().MaximumLength(20).WithMessage("Kod pocztowy jest wymagany");
            RuleFor(f => f.Number).NotEmpty().MaximumLength(20).WithMessage("Numer mieszkania jest wymagany");
        }
    }

    public class ClientFormProfile : Profile    
    {   
        public ClientFormProfile()
        {
            CreateMap<ClientForm , BankApplication.Data.Entities.Client>();
        }
    }
}
