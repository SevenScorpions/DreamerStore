namespace DreamerStore2.Models
{
    public class CartItem
    {
        public DetailedProduct DetailedProduct { get; set; }
        public int Quantity { get; set; }

        public CartItem(DetailedProduct detailedProduct, int quantity)
        {
            DetailedProduct = detailedProduct;
            Quantity = quantity;
        }
    }
}
