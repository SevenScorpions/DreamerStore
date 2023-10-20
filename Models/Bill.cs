using System;
using System.Collections.Generic;

namespace DreamerStore2.Models;

public partial class Bill
{
    public int BillId { get; set; }

    public string BillPhoneNumber { get; set; } = null!;

    public string BillLastName { get; set; } = null!;

    public string BillFirstName { get; set; } = null!;

    public string BillNote { get; set; } = null!;

    public string BillPostcode { get; set; } = null!;

    public string BillEmail { get; set; } = null!;

    public string BillProvince { get; set; } = null!;

    public string BillWard { get; set; } = null!;

    public string BillAddress { get; set; } = null!;

    public DateTime BillUpdatedAt { get; set; }

    public DateTime BillCreatedAt { get; set; }

    public int BillOldPrice { get; set; }

    public int BillTaxAmount { get; set; }

    public int BillPrice { get; set; }

    public int BillFinalPrice { get; set; }

    public int BillDiscountAmount { get; set; }

    public string? Meta { get; set; }

    public bool? Hide { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int BillTermOfPayment { get; set; }

    public int BillStt { get; set; }

    public virtual ICollection<BillProduct> BillProducts { get; set; } = new List<BillProduct>();

    public virtual BillStt BillSttNavigation { get; set; } = null!;

    public virtual TermOfPayment BillTermOfPaymentNavigation { get; set; } = null!;

    public virtual ICollection<DiscountUse> DiscountUses { get; set; } = new List<DiscountUse>();
}
