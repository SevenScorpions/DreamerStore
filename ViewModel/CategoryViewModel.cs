using DreamerStore2.Models;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace DreamerStore2.ViewModel
{
    public class CategoryViewModel
    {
        public CategoryViewModel(Category category) 
        {
            CategoryId = category.CategoryId;
            CategoryName = category.CategoryName;
            Order = category.Order;
            Meta = category.Meta;
            Image = category.Image;
            Hide = category.Hide;
            CreatedAt = category.CreatedAt;
            UpdatedAt = category.UpdatedAt;
            
        }
        public int CategoryId { get; set; }

        [Display(Name = "Tên danh mục")]
        public string CategoryName { get; set; } = null!;

        [Display(Name = "Thứ tự")]
        public int? Order { get; set; }

        [Display(Name = "Meta")]
        public string? Meta { get; set; }

        [Display(Name = "Hình ảnh")]
        public string? Image { get; set; }

        [Display(Name = "Hiển thị")]
        public bool? Hide { get; set; }

        [Display(Name = "Ngày tạo")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Ngày cập nhật")]
        public DateTime UpdatedAt { get; set; }
        public string CategoryImage { get; set; }
    }
}
