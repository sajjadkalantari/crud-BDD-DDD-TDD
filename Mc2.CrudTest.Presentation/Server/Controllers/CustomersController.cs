using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            return await _mediator.Send(createCustomerCommand);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteCustomerAsync(int id)
        {
            var deleteCommand = new DeleteCustomerCommand(id);
            return await _mediator.Send(deleteCommand);
        }
    }
}
