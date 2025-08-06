using AutoMapper;
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
        }       
    }
}
