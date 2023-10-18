using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DreamerStore2.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    [Display(Name = "Tên danh mục")]
    public string CategoryName { get; set; } = null!;

    [Display(Name = "Thứ tự")]
    public int? Order { get; set; }

    [Display(Name = "Meta")]
    public string? Meta { get; set; }

    [Display(Name = "Hình ảnh")]
    public string? Image { get; set; }

    [Display(Name = "Ẩn")]
    public bool? Hide { get; set; }

    [Display(Name = "Ngày tạo")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Ngày cập nhật")]
    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
