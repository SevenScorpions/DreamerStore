using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string DiscountName { get; set; } = null!;

    public string DiscountCalUnit { get; set; } = null!;

    public int DiscountCondition { get; set; }

    public int DiscountMaxValue { get; set; }

    public string DiscountRemark { get; set; } = null!;

    public DateTime DiscountAvailableFrom { get; set; }

    public DateTime DiscountAvailableUntil { get; set; }

    public int DiscountValue { get; set; }

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public string? Image { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<DiscountUse> DiscountUses { get; set; } = new List<DiscountUse>();
}
