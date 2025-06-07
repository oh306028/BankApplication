using AutoMapper;
using BankApplication.App.Modules.Client.Models.Details;
using FluentValidation;

namespace BankApplication.App.Modules.Client.Models.Create
{
    public class ClientForm
    {
        public Guid? PublicId { get; set; }  

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PESEL { get; set; }
        public DateTime? BirthDate { get; set; }
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
            RuleFor(f => f.FirstName)
                .NotEmpty().WithMessage("Imię jest wymagane")
                .MaximumLength(50).WithMessage("Imię nie może mieć więcej niż 50 znaków");

            RuleFor(f => f.LastName)
                .NotEmpty().WithMessage("Nazwisko jest wymagane")
                .MaximumLength(50).WithMessage("Nazwisko nie może mieć więcej niż 50 znaków");

            RuleFor(f => f.Email)
                .NotEmpty().WithMessage("Email jest wymagany")
                .MaximumLength(50).WithMessage("Email nie może mieć więcej niż 50 znaków");

            RuleFor(f => f.Phone)
                .NotEmpty().WithMessage("Numer telefonu jest wymagany")
                .MaximumLength(20).WithMessage("Numer telefonu nie może mieć więcej niż 20 znaków");

            RuleFor(f => f.PESEL)
                .NotEmpty().WithMessage("PESEL jest wymagany")
                .MaximumLength(20).WithMessage("PESEL nie może mieć więcej niż 20 znaków");

            RuleFor(f => f.Nationality)
                .NotEmpty().WithMessage("Narodowość jest wymagana")
                .MaximumLength(20).WithMessage("Narodowość nie może mieć więcej niż 20 znaków");

            RuleFor(f => f.Country)
                .NotEmpty().WithMessage("Państwo jest wymagane")
                .MaximumLength(30).WithMessage("Państwo nie może mieć więcej niż 30 znaków");

            RuleFor(f => f.City)
                .NotEmpty().WithMessage("Miasto jest wymagane")
                .MaximumLength(50).WithMessage("Miasto nie może mieć więcej niż 50 znaków");

            RuleFor(f => f.PostalCode)
                .NotEmpty().WithMessage("Kod pocztowy jest wymagany")
                .MaximumLength(20).WithMessage("Kod pocztowy nie może mieć więcej niż 20 znaków");

            RuleFor(f => f.Number)
                .NotEmpty().WithMessage("Numer mieszkania jest wymagany")
                .MaximumLength(20).WithMessage("Numer mieszkania nie może mieć więcej niż 20 znaków");

            RuleFor(b => b.BirthDate).NotEmpty().WithMessage("Data urodzenia jest wymagana");
               
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
