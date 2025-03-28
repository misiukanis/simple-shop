using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.Commands.UpdateCustomer;
using Shop.Application.DTOs;
using Shop.Application.Queries.GetCustomerById;
using Shop.Application.Queries.GetCustomers;
using Shop.Models.Requests;
using Shop.Shared.Constants;

namespace Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomersController(
        ISender mediator) : Controller
    {
        private readonly ISender _mediator = mediator;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customersDto = await _mediator.Send(new GetCustomersQuery());
            return Ok(customersDto);
        }

        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(Guid customerId)
        {
            var customerDto = await _mediator.Send(new GetCustomerByIdQuery(customerId));
            if (customerDto == null)
            {
                return NotFound();
            }

            return Ok(customerDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateCustomer(CreateCustomerRequest customerRequest)
        {
            var customerCmd = new CreateCustomerCommand(Guid.NewGuid(), customerRequest.Name);
            await _mediator.Send(customerCmd);

            return CreatedAtAction(nameof(GetCustomerById), ControllerConstants.Customers, 
                new { customerId = customerCmd.Id }, new { id = customerCmd.Id });
        }

        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer(Guid customerId, UpdateCustomerRequest customerRequest)
        {
            if (customerId != customerRequest.Id)
            {
                return BadRequest();
            }

            var customerCmd = new UpdateCustomerCommand(customerRequest.Id, customerRequest.Name);
            await _mediator.Send(customerCmd);

            return NoContent();
        }
    }
}
