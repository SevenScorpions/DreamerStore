using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class BillProduct
{
    public int BillId { get; set; }

    public int DetailedProductId { get; set; }

    public int Amount { get; set; }

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual DetailedProduct DetailedProduct { get; set; } = null!;
}
