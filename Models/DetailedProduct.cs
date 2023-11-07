using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class DetailedProduct
{
    public int DetailedProductId { get; set; }

    public decimal DetailedProductPrice { get; set; }

    public int DetailedProductQuantity { get; set; }

    public string DetailedProductName { get; set; } = null!;

    public int ProductId { get; set; }

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public string? Image { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BillProduct> BillProducts { get; set; } = new List<BillProduct>();

    public virtual Product Product { get; set; } = null!;
}
