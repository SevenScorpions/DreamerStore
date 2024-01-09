namespace DreamerStore2.Models
{
    public class Cart
    {
        public Dictionary<int, OrderedDetailedProduct> ProductList {  get; set; }
        public decimal CartTotalPrice { get; set; }    }
}
