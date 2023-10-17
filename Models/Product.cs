using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductDescription { get; set; } = null!;

    public int ProductSold { get; set; }

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<DetailedProduct> DetailedProducts { get; set; } = new List<DetailedProduct>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
