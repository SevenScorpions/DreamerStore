using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class BillStt
{
    public int BillSttId { get; set; }

    public int BillSttName { get; set; }

    public int? Order { get; set; }

    public string? Meta { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
