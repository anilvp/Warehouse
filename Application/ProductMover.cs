using Application.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class ProductMover : IMoveProducts
{
    private readonly IUnitOfWork _uow;
    private readonly IGenericRepository<ProductGroupContent> _productGroupContentsRepo;
    private readonly IGenericRepository<Location> _locationsRepo;
    private readonly IGenericRepository<ProductGroup> _productGroupsRepo;

    public ProductMover(IUnitOfWork uow, IGenericRepository<ProductGroupContent> productGroupContentsRepo,
                                          IGenericRepository<Location> locationsRepo,
                                          IGenericRepository<ProductGroup> productGroupsRepo)
    {
        _uow = uow;
        _productGroupContentsRepo = productGroupContentsRepo;
        _locationsRepo = locationsRepo;
        _productGroupsRepo = productGroupsRepo;
    }

    public void Move(MoveProductGroupDto moveProductGroupDto)
    {
        var productGroup = _productGroupsRepo.GetById(moveProductGroupDto.ProductGroupId);
        var newLocation = _locationsRepo.Get(l => l.LocationId == moveProductGroupDto.LocationId).Include(l => l.ProductGroup).SingleOrDefault();

        newLocation.ChangeProductGroup(productGroup);

        _uow.Save();
    }

    public void Move(MoveProductDto moveProductDto)
    {
        var productGroupContent = _productGroupContentsRepo.Get(pgc => pgc.ProductGroupId == moveProductDto.ProductGroupId &&
                                                                       pgc.ProductId == moveProductDto.ProductId &&
                                                                       pgc.DeliveryBatchId == moveProductDto.DeliveryBatchId).SingleOrDefault();
        var newLocation = _locationsRepo.Get(l => l.LocationId == moveProductDto.LocationId).Include(l => l.ProductGroup).ThenInclude(pg => pg.ProductGroupContents).SingleOrDefault();

        productGroupContent.MoveProductGroupContent(newLocation, moveProductDto.Quantity);

        if (productGroupContent.Quantity == 0)
        {
            _productGroupContentsRepo.Delete(productGroupContent);
        }
        var oldProductGroup = _productGroupsRepo.Get(pg => pg.ProductGroupId == moveProductDto.ProductGroupId).Include(pg => pg.ProductGroupContents).SingleOrDefault();
        if (oldProductGroup.ProductGroupContents.Count == 0)
        {
            _productGroupsRepo.Delete(oldProductGroup);
        }

        _uow.Save();
    }
}