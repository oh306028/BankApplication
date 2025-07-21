using BankApplication.App.Helpers.Extensions;
using BankApplication.App.Modules.BankAccount.Models.Update;
using BankApplication.App.Services.BankAccount;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.BankAccount.Controllers
{
    [Route("api/bank-accounts")]
    [ApiController]
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

    }
}
