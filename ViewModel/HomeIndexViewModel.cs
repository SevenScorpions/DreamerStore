using DreamerStore2.Models;

namespace DreamerStore2.ViewModel
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel(List<Category> categories, List<Product> products, List<DetailedProduct> detailedProducts)
        {
            this.categories = categories;
            this.products = products;
            this.detailedProducts = detailedProducts;
        }

        public List<Category> categories { get; set; }
        public List<Product> products { get; set; }
        public List<DetailedProduct> detailedProducts { get; set; }

    }
}
