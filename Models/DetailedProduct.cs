using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DreamerStore2.Models;

public partial class DetailedProduct
{
    public int DetailedProductId { get; set; }

    [DisplayName("Giá CTPS")]
    public decimal DetailedProductPrice { get; set; }

    [DisplayName("Số lượng")]
    public int DetailedProductQuantity { get; set; }

    [DisplayName("Tên CTSP")]
    public string DetailedProductName { get; set; } = null!;
    
    public int ProductId { get; set; }

    [DisplayName("Thumbnail")]
    public string? Image { get; set; }

    [DisplayName("Thứ tự hiển thị")]
    public int? Order { get; set; }

    [DisplayName("Meta")]
    public string? Meta { get; set; }

    [DisplayName("Hiển thị")]
    public bool? Hide { get; set; }

    [DisplayName("Ngày tạo")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Ngày cập nhật")]
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<BillProduct> BillProducts { get; set; } = new List<BillProduct>();

    public virtual Product Product { get; set; } = null!;
}
