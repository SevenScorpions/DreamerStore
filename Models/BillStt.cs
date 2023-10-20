using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class BillStt
{
    public int BillSttId { get; set; }

    public int BillSttName { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}
