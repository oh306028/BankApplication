using AutoMapper;
using BankApplication.App.Modules.BankAccount.Models.Details;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.Data.Entities;

namespace BankApplication.App
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BankApplication.Data.Entities.BankAccount, BankAccountDetails>()
                .ForMember(p => p.InterestRate, o => o.MapFrom(s => s.InteresetRate))
                .ForMember(p => p.AccountNumber, o => o.MapFrom(s => s.BankAccountNumber))
                .ForMember(p => p.PublicId, o => o.MapFrom(s => s.PublicId.ToString()));

            CreateMap<SendTransferModel, Transfer>()
                .ForSourceMember(p => p.AccountToNumber, o => o.DoNotValidate());
        }
    }
}
