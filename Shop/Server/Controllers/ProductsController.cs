using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Commands.CreateProduct;
using Shop.Application.Commands.DeleteProduct;
using Shop.Application.Commands.UpdateProduct;
using Shop.Application.Exceptions;
using Shop.Application.Queries.GetProductById;
using Shop.Application.Queries.GetProducts;
using Shop.Shared.Constants;
using Shop.Shared.DTOs;

namespace Shop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductsController : Controller
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public ProductsController(ISender mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var productsDto = await _mediator.Send(new GetProductsQuery());
            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProductDTO>> GetProductById(Guid id)
        {
            var productDto = await _mediator.Send(new GetProductByIdQuery(id));
            if (productDto == null)
            {
                return NotFound();
            }

            return Ok(productDto);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateProduct(CreateProductDTO productDto)
        {
            var productCmd = new CreateProductCommand(Guid.NewGuid(), productDto.Name, productDto.Price);
            await _mediator.Send(productCmd);

            return CreatedAtAction(nameof(GetProductById), ControllerNameConstants.Products, new { id = productCmd.Id }, productCmd);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateProduct(Guid id, UpdateProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }

            var productCmd = new UpdateProductCommand(productDto.Id, productDto.Name, productDto.Price);

            try
            {
                await _mediator.Send(productCmd);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _mediator.Send(new DeleteProductCommand(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
