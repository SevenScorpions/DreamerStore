using DreamerStore2.Models;

namespace DreamerStore2.ViewModel
{
    public class DetailedProductViewModel
    {
        public DetailedProductViewModel(IEnumerable<DetailedProduct> detailedProducts,Product product) {
            DetailedProducts = detailedProducts;
            Product = product;
        }
        public IEnumerable<DetailedProduct> DetailedProducts { get; set; }
        public Product Product { get; set; }
    }
}
