namespace DreamerStore2.Models
{
    public class OrderedDetailedProduct
    {
        public DetailedProduct DetailedProduct { get; set; }
        public int Quantity { get; set; }

        public OrderedDetailedProduct(DetailedProduct detailedProduct, int quantity)
        {
            DetailedProduct = detailedProduct;
            Quantity = quantity;
        }
    }
}
