using Dtos;

namespace Application.Interfaces;

public interface IReplenishProducts
{
    void Replenish(ReplenishDto replenishDto);
}