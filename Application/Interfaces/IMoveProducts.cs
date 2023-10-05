using Dtos;

namespace Application.Interfaces;

public interface IMoveProducts
{
    void Move(MoveProductGroupDto moveProductDto);

    void Move(MoveProductDto moveProductDto);
}