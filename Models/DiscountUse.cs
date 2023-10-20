using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class DiscountUse
{
    public int DiscountId { get; set; }

    public int BillId { get; set; }

    public DateTime UsedAt { get; set; }

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Discount Discount { get; set; } = null!;
}
