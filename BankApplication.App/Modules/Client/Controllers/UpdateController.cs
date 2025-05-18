using AutoMapper;
using BankApplication.App.Modules.Account.Controllers;
using BankApplication.App.Modules.Client.Models.Create;
using BankApplication.App.Modules.Client.Models.Details;
using BankApplication.App.Services.Account;
using BankApplication.App.Services.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankApplication.App.Modules.Client.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IUpdateService service;
        private readonly IMapper mapper;

        public UpdateController(IUpdateService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult Join([FromBody] ClientForm model)
        {
            var request = service.Join(model);
            return CreatedAtAction(nameof(DetailsController.Fetch), "Details", new { Id = request.PublicId }, mapper.Map<ClientDetails>(request));
                
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public ActionResult Update([FromBody] ClientForm model)
        {
            service.Update(model);
            return Accepted();
        }
    }
}
