﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Domain;

public class DeliveryBatchContent
{
    private DeliveryBatchContent()
    {
        ProductGroupContents = new List<ProductGroupContent>();
    }

    public int ProductId { get; private set; }

    public int DeliveryBatchId { get; private set; }

    public int? Quantity { get; private set; }

    public virtual DeliveryBatch DeliveryBatch { get; private set; }

    public virtual Product Product { get; private set; }

    public virtual ICollection<ProductGroupContent> ProductGroupContents { get; private set; }
}