﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Domain;
using DataAccess.Mapping;

namespace DataAccess;

public partial class WarehouseContext : DbContext
{
    public WarehouseContext()
    {
    }

    public WarehouseContext(DbContextOptions<WarehouseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DeliveryBatch> DeliveryBatches { get; set; }

    public virtual DbSet<DeliveryBatchContent> DeliveryBatchContents { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<LocationType> LocationTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<ProductGroupContent> ProductGroupContents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new DeliveryBatchMap());
        modelBuilder.ApplyConfiguration(new DeliveryBatchContentMap());
        modelBuilder.ApplyConfiguration(new LocationMap());
        modelBuilder.ApplyConfiguration(new LocationTypeMap());
        modelBuilder.ApplyConfiguration(new ProductMap());
        modelBuilder.ApplyConfiguration(new ProductGroupMap());
        modelBuilder.ApplyConfiguration(new ProductGroupContentMap());
    }
}