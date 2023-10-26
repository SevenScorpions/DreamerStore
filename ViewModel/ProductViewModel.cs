using DreamerStore2.Models;

namespace DreamerStore2.ViewModel
{
    public class ProductViewModel
    {
        public ProductViewModel(Product product) {
            ProductId = product.ProductId;
            ProductName = product.ProductName;
            ProductDescription = product.ProductDescription;
            ProductSold = product.ProductSold;
            Order = product.Order;
            Meta = product.Meta;
            Hide = product.Hide;
            DetailedProducts = product.DetailedProducts;
            ProductImages = product.ProductImages;
            CategoryId = product.CategoryId;
            Category = product.Category;
        }
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public int ProductSold { get; set; }

        public string? Image { get; set; }

        public int? Order { get; set; }

        public string? Meta { get; set; }

        public bool? Hide { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<DetailedProduct> DetailedProducts { get; set; } = new List<DetailedProduct>();

        public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    }
}
