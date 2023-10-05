namespace Dtos;

public class MoveProductDto
{
    public int ProductGroupId { get; set; }

    public int ProductId { get; set; }

    public int DeliveryBatchId { get; set; }

    public int Quantity { get; set; }

    public int LocationId { get; set; }
}