using Application.Interfaces;
using Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/productReplenish")]
[ApiController]
public class ProductReplenishController : ControllerBase
{
    private readonly IReplenishProducts _replenishProducts;

    public ProductReplenishController(IReplenishProducts replenishProducts)
    {
        _replenishProducts = replenishProducts;
    }

    [HttpPost]
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
