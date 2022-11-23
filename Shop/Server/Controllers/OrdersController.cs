using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Commands.ChangeOrderStatus;
using Shop.Application.Commands.CreateOrder;
using Shop.Application.Exceptions;
using Shop.Application.Queries.GetOrderById;
using Shop.Application.Queries.GetOrders;
using Shop.Application.Queries.GetProductById;
using Shop.Shared.Constants;
using Shop.Shared.DTOs;
using Shop.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrdersController : Controller
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public OrdersController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
        {
            var ordersDto = await _mediator.Send(new GetOrdersQuery());
            return Ok(ordersDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OrderDTO>> GetOrderById(Guid id)
        {
            var orderDto = await _mediator.Send(new GetOrderByIdQuery(id));
            if (orderDto == null)
            {
                return NotFound();
            }

            return Ok(orderDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO orderDto)
        {
            var orderItemsCmd = new List<CreateOrderItemCommand>();
            foreach (var orderItemDto in orderDto.OrderItems)
            {
                var productDto = await _mediator.Send(new GetProductByIdQuery(orderItemDto.ProductId));
                orderItemsCmd.Add(new CreateOrderItemCommand(orderItemDto.ProductId, orderItemDto.Quantity, productDto.Price));
            }

            var orderCmd = new CreateOrderCommand(Guid.NewGuid(), orderDto.CustomerId.Value, orderDto.City, orderDto.Street, orderItemsCmd);
            await _mediator.Send(orderCmd);

            return CreatedAtAction(nameof(GetOrderById), ControllerNameConstants.Orders, new { id = orderCmd.Id }, orderCmd);
        }

        [HttpPut("{id}/Status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeOrderStatus(Guid id, [FromBody][EnumDataType(typeof(OrderStatus))] OrderStatus orderStatus)
        {
            try
            {
                await _mediator.Send(new ChangeOrderStatusCommand(id, orderStatus));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        
    }
}
