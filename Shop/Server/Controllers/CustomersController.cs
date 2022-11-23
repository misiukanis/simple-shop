using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.Commands.UpdateCustomer;
using Shop.Application.Exceptions;
using Shop.Application.Queries.GetCustomerById;
using Shop.Application.Queries.GetCustomers;
using Shop.Shared.Constants;
using Shop.Shared.DTOs;

namespace Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CustomersController : Controller
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public CustomersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            var customersDto = await _mediator.Send(new GetCustomersQuery());
            return Ok(customersDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(Guid id)
        {
            var customerDto = await _mediator.Send(new GetCustomerByIdQuery(id));
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
        public async Task<IActionResult> CreateCustomer(CreateCustomerDTO customerDto)
        {
            var customerCmd = new CreateCustomerCommand(Guid.NewGuid(), customerDto.Name);
            await _mediator.Send(customerCmd);

            return CreatedAtAction(nameof(GetCustomerById), ControllerNameConstants.Customers, new { id = customerCmd.Id }, customerCmd);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer(Guid id, UpdateCustomerDTO customerDto)
        {
            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            var customerCmd = new UpdateCustomerCommand(customerDto.Id, customerDto.Name);

            try
            {
                await _mediator.Send(customerCmd);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
