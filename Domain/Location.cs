﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain;

public class Location
{
    private Location() { }

    public void ChangeProductGroup(ProductGroup productGroup)
    {
        ProductGroup = productGroup;
    }

    public int LocationId { get; private set; }

    public string Code { get; private set; }

    public int? LocationTypeId { get; private set; }

    public int? DefaultProductId { get; private set; }

    public Product DefaultProduct { get; private set; }

    public LocationType LocationType { get; private set; }

    public ProductGroup ProductGroup { get; private set; }
}