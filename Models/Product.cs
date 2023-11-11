using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DreamerStore2.Models;

public partial class Product
{
    [DisplayName("ID")]
    public int ProductId { get; set; }

    [DisplayName("Tên sản phẩm")]
    public string ProductName { get; set; } = null!;

    [DisplayName("Mô tả sản phẩm")]
    public string? ProductDescription { get; set; }

    [DisplayName("Số lượng đã bán")]
    public int ProductSold { get; set; }

    [DisplayName("Giá sản phẩm")]
    public decimal ProductPrice { get; set; }

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

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<DetailedProduct> DetailedProducts { get; set; } = new List<DetailedProduct>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
