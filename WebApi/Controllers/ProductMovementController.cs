using Application.Interfaces;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/productMovement")]
[ApiController]
public class ProductMovementController : ControllerBase
{
    private readonly IMoveProducts _moveProducts;
    private readonly IReplenishProducts _replenishProducts;

    public ProductMovementController(IMoveProducts moveProducts, IReplenishProducts replenishProducts)
    {
        _moveProducts = moveProducts;
        _replenishProducts = replenishProducts;
    }

    [HttpPost("moveProductGroup")]
    public IActionResult MoveProductGroup([FromBody] MoveProductGroupDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }
        _moveProducts.Move(dto);
        return Ok();
    }

    [HttpPost("moveProduct")]
    public IActionResult MoveProduct([FromBody] MoveProductDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }
        _moveProducts.Move(dto);
        return Ok();
    }

    [HttpPost("replenishProduct")]
    public IActionResult ReplenishProduct([FromBody] ReplenishDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }
        _replenishProducts.Replenish(dto);
        return Ok();
    }

}
