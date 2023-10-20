using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class ProductImage
{
    public int ProductImageId { get; set; }

    public string? ProductImageLink { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
