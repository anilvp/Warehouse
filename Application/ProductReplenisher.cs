using Application.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using Domain;
using Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class ProductReplenisher : IReplenishProducts
{
    private readonly IUnitOfWork _uow;
    private readonly IGenericRepository<ProductGroup> _productGroupsRepo;
    private readonly IGenericRepository<ProductGroupContent> _productGroupContentsRepo;
    private readonly IGenericRepository<Location> _locationsRepo;
    private readonly IGenericRepository<LocationType> _locationTypesRepo;

    public ProductReplenisher(IUnitOfWork uow, IGenericRepository<ProductGroup> productGroupsRepo,
                                               IGenericRepository<Location> locationsRepo,
                                               IGenericRepository<LocationType> locationTypesRepo,
                                               IGenericRepository<ProductGroupContent> productGroupContentsRepo)
    {
        _uow = uow;
        _productGroupsRepo = productGroupsRepo;
        _locationsRepo = locationsRepo;
        _locationTypesRepo = locationTypesRepo;
        _productGroupContentsRepo = productGroupContentsRepo;
    }

    public void Replenish(ReplenishDto replenishDto)
    {
        int holdingZoneId = _locationTypesRepo.Get(lt => lt.LocationType1 == "Holding Zone")
                                              .Select(lt => lt.LocationTypeId)
                                              .SingleOrDefault();
        int deepStorageId = _locationTypesRepo.Get(lt => lt.LocationType1 == "Deep Storage")
                                              .Select(lt => lt.LocationTypeId)
                                              .SingleOrDefault();
        int storageId = _locationTypesRepo.Get(lt => lt.LocationType1 == "Storage")
                                              .Select(lt => lt.LocationTypeId)
                                              .SingleOrDefault();
        int pickingId = _locationTypesRepo.Get(lt => lt.LocationType1 == "Picking")
                                              .Select(lt => lt.LocationTypeId)
                                              .SingleOrDefault();
        int?[] storageLocationTypeIds = { holdingZoneId, deepStorageId, storageId, pickingId };

        //Get default locations for the given product
        var productDefaultLocations = _locationsRepo.Get(l => l.DefaultProductId == replenishDto.ProductId)
                                                    .OrderByDescending(l => l.LocationTypeId)
                                                    .Include(l => l.ProductGroup)
                                                    .ToList();

        //Get all product groups in Holding Zone, Deep Storage, Storage and Picking that contain the given product
        var productGroupIds = _productGroupContentsRepo.Get(pgc => pgc.ProductId == replenishDto.ProductId)
                                                       .Where(pgc => storageLocationTypeIds.Contains(pgc.ProductGroup.Location.LocationTypeId))
                                                       .Select(pgc => pgc.ProductGroupId)
                                                       .ToList();
        var productGroups = _productGroupsRepo.Get(pg => productGroupIds.Contains(pg.ProductGroupId))
                                              .OrderByDescending(pg => pg.Location.LocationTypeId)
                                              .ToList();

        //Fill default locations by shuffling stock forward
        int count1 = Math.Min(productDefaultLocations.Count, productGroups.Count);
        for (int i = 0; i < count1; i++)
        {
            productDefaultLocations[i].ChangeProductGroup(productGroups[i]);
        }

        //Shuffle any remaining stock to Deep Storage
        if (productGroups.Count > count1)
        {
            var deepStorageLocations = _locationsRepo.Get(l => l.LocationTypeId == deepStorageId)
                                                     .OrderBy(l => l.Code)
                                                     .Include(l => l.ProductGroup)
                                                     .Where(l => l.ProductGroup == null)
                                                     .ToList();
            int count2 = Math.Min(productGroups.Count - count1, deepStorageLocations.Count);
            for (int i = 0; i < count2; i++)
            {
                deepStorageLocations[i].ChangeProductGroup(productGroups[count1 + i]);
            }
        }

        _uow.Save();
    }
}