using AutoMapper;
using BankApplication.App.Modules.Account.Models.Details;
using BankApplication.App.Modules.BankAccount.Models.Details;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Resolvers;
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

            CreateMap<Transfer, TransferDetails>()
                .ForMember(p => p.Sender, o => o.MapFrom<SenderResolver>())
                .ForMember(p => p.Receiver, o => o.MapFrom<ReceiverResolver>())
                .ForMember(p => p.ReceiverNumber, o => o.MapFrom<ReceiverNumberResolver>())     
                .ForMember(p => p.SenderNumber, o => o.MapFrom<SenderNumberResolver>());

            CreateMap<Logging, LogginDetails>() 
                .ForMember(p => p.ClientName, o => o.MapFrom<ClientNameResolver>());

            CreateMap<BankAccountBlockRequest, BlockRequestDetails>()
                .ForMember(p => p.ClientName, o => o.MapFrom<ClientNameResolver>())
                .ForMember(p => p.BankAccountNumber, o => o.MapFrom<AccountNumberResolver>())
                .ForMember(p => p.AccountId, o => o.MapFrom<AccountIdResolver>());

            CreateMap<Account, ProfileDetails>()
                .ForMember(p => p.FirstName, o => o.MapFrom(s => s.Client.FirstName))
                .ForMember(p => p.LastName, o => o.MapFrom(s => s.Client.LastName))
                .ForMember(p => p.Email, o => o.MapFrom(s => s.Client.Email))
                .ForMember(p => p.Phone, o => o.MapFrom(s => s.Client.Phone))
                .ForMember(p => p.PESEL, o => o.MapFrom(s => s.Client.PESEL))
                .ForMember(p => p.BirthDate, o => o.MapFrom(s => s.Client.BirthDate))
                .ForMember(p => p.Country, o => o.MapFrom(s => s.Client.Country))
                .ForMember(p => p.City, o => o.MapFrom(s => s.Client.City))
                .ForMember(p => p.PostalCode, o => o.MapFrom(s => s.Client.PostalCode))
                .ForMember(p => p.Number, o => o.MapFrom(s => s.Client.Number));
        }       
    }
}
