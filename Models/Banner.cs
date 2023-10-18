using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DreamerStore2.Models;

public partial class Banner
{
    public int BannerId { get; set; }

    [DisplayName("Tên Banner")]
    public string BannerName { get; set; } = null!;

    [DisplayName("Thông điệp Banner")]
    public string BannerMessage { get; set; } = null!;

    [DisplayName("Mô tả Banner")]
    public string BannerDescription { get; set; } = null!;

    [DisplayName("Thứ tự")]
    public int? Order { get; set; }

    [DisplayName("Meta")]
    public string? Meta { get; set; }

    [DisplayName("Hình ảnh")]
    public string? Image { get; set; }

    [DisplayName("Ẩn")]
    public bool? Hide { get; set; }

    [DisplayName("Ngày tạo")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Ngày cập nhật")]
    public DateTime UpdatedAt { get; set; }
}
