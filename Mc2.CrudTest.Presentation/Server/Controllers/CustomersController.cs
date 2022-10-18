using Mc2.CrudTest.Presentation.Application.Commands;
using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ICustomerQueries _customerQueries;

        public CustomersController(IMediator mediator, ICustomerQueries customerQueries)
        {
            _mediator = mediator;
            _customerQueries = customerQueries;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomerAsync(int id)
        {
            return await _customerQueries.GetCustomersByIdAsync(id);
        }


        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomersAsync()
        {
            var result =  await _customerQueries.GetCustomersAsync();
            return Ok(result);
        }


        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> CreateCustomerAsync([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            return await _mediator.Send(createCustomerCommand);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomerAsync([FromBody] UpdateCustomerCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<int>> DeleteCustomerAsync(int id)
        {
            var deleteCommand = new DeleteCustomerCommand(id);
            return await _mediator.Send(deleteCommand);
        }

    }
}
