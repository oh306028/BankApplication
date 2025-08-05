using AutoMapper;
using BankApplication.App.Modules.BankAccount.Models.Details;
using BankApplication.App.Modules.BankAccount.Transfers.Models;
using BankApplication.App.Services.BankAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.BankAccount.Controllers
{

    [Route("api/bank-accounts/dictionary")]
    [ApiController]
    [Authorize]
    public class DictionaryController : ControllerBase
    {

        private readonly IUpdateService transfers;
        private readonly IDetailService transfersDetails;

        private readonly IMapper mapper;

        public DictionaryController(IUpdateService update, IDetailService details, IMapper mapper)
        {
            transfers = update;
            transfersDetails = details;
            this.mapper = mapper;
        }

        [HttpGet("transfers")]
        public ActionResult<List<TransferDetails>> GetAllTransfers()    
        {
            var transfers = transfersDetails.GetAll();
            return Ok(mapper.Map<List<TransferDetails>>(transfers));
        }

        [HttpGet("list")]
        public ActionResult<List<BankAccountDetails>> GetAllBankAccounts()  
        {
            var accounts = transfersDetails.GetAllAccounts();   
            return Ok(mapper.Map<List<BankAccountDetails>>(accounts));
        }

    }
}
