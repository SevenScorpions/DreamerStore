using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class TermOfPayment
{
    public int PaymentId { get; set; }

    public string PaymentName { get; set; } = null!;

    public string? Meta { get; set; }

    public string? Image { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
