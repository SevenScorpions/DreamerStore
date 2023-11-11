using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DreamerStore2.Models;

public partial class Category
{
    public int CategoryId { get; set; }
    
    [DisplayName("Tên danh mục")]
    public string CategoryName { get; set; } = null!;

    [DisplayName("Thứ tự hiển thị")]
    public int? Order { get; set; }

    [DisplayName("Meta")]
    public string? Meta { get; set; }

    [DisplayName("Thumbnail")]
    public string? Image { get; set; }

    [DisplayName("Hiển thị")]
    public bool? Hide { get; set; }

    [DisplayName("Ngày tạo")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Ngày cập nhật")]
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
