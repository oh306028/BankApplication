using AutoMapper;
using BankApplication.App.Helpers.Extensions;
using BankApplication.App.Helpers.Models;
using BankApplication.App.Modules.BankAccount.Models.Details;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Printers;
using BankApplication.App.Services.BankAccount;
using BankApplication.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace BankApplication.App.Modules.BankAccount.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
    [Authorize]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailService service;

        private readonly IMapper mapper;

        public DetailsController(IDetailService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet()]

        public ActionResult<bool> HasBankAccount()
        {
            var bankAccount = service.Fetch(User.Id());  
            
            return Ok(bankAccount.Any());
        }

        [HttpGet("{type}")]
        public ActionResult<BankAccountDetails> GetDetailsByType(BankAccountType type)
        {
            var details = service.GetDetailsByType(User.Id(), type);

            return Ok(mapper.Map<BankAccountDetails>(details));
        }


        [HttpGet("own-types")]
        public ActionResult<List<GenericKeyValuePair>> GetOwnTypesOfAccounts()
        {
            var list = service.GetOwnTypes(User.Id());

            var enums = Enum.GetValues(typeof(BankAccountType))
                   .Cast<BankAccountType>()
                   .Select(e => new GenericKeyValuePair(e))
                   .ToList();

            return enums.Where(p => list.Contains(p.Key)).ToList();
        }


        [HttpGet("types")]
        public ActionResult<List<GenericKeyValuePair>> GetTypes()
        {
            var list = Enum.GetValues(typeof(BankAccountType))
                   .Cast<BankAccountType>()
                   .Select(e => new GenericKeyValuePair(e))
                   .ToList();

            return Ok(list);

        }

        [HttpGet("rates")]
        public ActionResult<List<GenericKeyValuePair>> GetInterestRates()   
        {
            var list = Enum.GetValues(typeof(InterestRate))
                   .Cast<InterestRate>()
                   .Select(e => new GenericKeyValuePair(e))
                   .ToList();   

            return Ok(list);

        }
            
        [HttpGet("currencies")]
        public ActionResult<List<GenericKeyValuePair>> GetCurrencies()
        {
            var list = Enum.GetValues(typeof(Currency))
                   .Cast<Currency>()
                   .Select(e => new GenericKeyValuePair(e))
                   .ToList();

            return Ok(list);

        }

        [HttpGet("credits")]
        public ActionResult<List<GenericKeyValuePair>> GetCreditAmounts()
        {
            var list = Enum.GetValues(typeof(CreditAmount))
                   .Cast<CreditAmount>()
                   .Select(e => new GenericKeyValuePair(e))
                   .ToList();
                
            return Ok(list);    

        }

        [HttpGet("{accountId}/has-active-block-request")]
        public ActionResult<bool> HasBlockRequests(Guid accountId)     
        {
            var hasAciveBlockRequest = service.HasActiveBlockRequests(accountId);
            return Ok(hasAciveBlockRequest);

        }

        [HttpGet("{accountId}/isBlocked")]
        public ActionResult<bool> IsBlocked(Guid accountId)
        {
            var result = service.IsBlocked(accountId);
            return Ok(result);
        }

        [HttpGet("{accountId}/download")]
        public ActionResult GetPdf([FromRoute] Guid accountId)
        {
            var account = service.FetchDetail(accountId);
            var printer = new BankAccountDetailsPrinter(account);   
            var pdfBytes = printer.GeneratePdf();

            return File(pdfBytes, "application/pdf", $"Wyciąg-{DateTime.Now.Date}.pdf");
        }

    }
}
