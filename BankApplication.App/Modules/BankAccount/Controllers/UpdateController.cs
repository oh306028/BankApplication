using BankApplication.App.Helpers.Extensions;
using BankApplication.App.Modules.BankAccount.Models.Update;
using BankApplication.App.Services.BankAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.BankAccount.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
    [Authorize]
    public class UpdateController : ControllerBase
    {
        private readonly IUpdateService service;

        public UpdateController(IUpdateService service)
        {
            this.service = service;
        }

        [HttpPost]
        public ActionResult CreateBankAccount([FromBody] CreateBankAccountModel model)
        {
            service.Create(User.Id(), model);
            return Ok();
        }

        [HttpPost("{accountId}/block-request")]
        public ActionResult SendBlockRequest(Guid accountId)
        {   
            service.SendBlockRequest(accountId); 
            return Ok();
        }

    }
}
