using BankApplication.App.Helpers.Extensions;
using BankApplication.App.Helpers.Models;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Services.BankAccount;
using BankApplication.Data.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.BankAccount.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailService service;

        public DetailsController(IDetailService service)
        {
            this.service = service;
        }

        [HttpGet()]
        [Authorize]
        public ActionResult<bool> HasBankAccount()
        {
            var bankAccount = service.Fetch(User.Id());  
            
            return Ok(bankAccount.Any());
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
      
    }
}
