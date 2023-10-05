﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain;

public class ProductGroup
{
    private ProductGroup()
    {
        ProductGroupContents = new List<ProductGroupContent>();
    }

    public ProductGroup(int locationId) : this()
    {
        LocationId = locationId;
    }

    public ProductGroupContent AddProductGroupContent(int productId, int deliveryBtachId, int quantity)
    {
        var productGroupContent = ProductGroupContents.Where(pgc => pgc.ProductGroupId == ProductGroupId && pgc.ProductId == productId && pgc.DeliveryBatchId == deliveryBtachId).SingleOrDefault();
        if (productGroupContent == null)
        {
            productGroupContent = new ProductGroupContent(ProductGroupId, productId, deliveryBtachId, quantity);
            ProductGroupContents.Add(productGroupContent);
        }
        else
        {
            productGroupContent.AddQuantity(quantity);
        }
        return productGroupContent;
    }

    public int ProductGroupId { get; private set; }

    public int? LocationId { get; private set; }

    public Location Location { get; private set; }

    public ICollection<ProductGroupContent> ProductGroupContents { get; private set; } 
}