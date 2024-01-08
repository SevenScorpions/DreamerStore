namespace DreamerStore2.Models
{
    public class Cart
    {
        Dictionary<int, OrderedDetailedProduct>? ProductList {  get; set; }
        public decimal CartTotalPrice { get; set; }
        public bool AddDetailedProductToCart (DetailedProduct product)
        {
            bool isAdded = false;
            if(ProductList != null)
            {
                if(!this.ProductList.ContainsKey(product.ProductId) && this.ProductList[product.ProductId].DetailedProduct.DetailedProductQuantity > 0)
                {
                    this.ProductList.Add(product.ProductId, new OrderedDetailedProduct(product, 1));
                    this.CartTotalPrice += this.ProductList[product.ProductId].DetailedProduct.DetailedProductPrice;
                    isAdded = true;
                } else
                {
                    if(product.DetailedProductQuantity > this.ProductList[product.ProductId].Quantity)
                    {
                        OrderedDetailedProduct orderedDetailedProduct = this.ProductList[product.ProductId];
                        this.ProductList[product.ProductId].Quantity += 1;
                        this.CartTotalPrice += orderedDetailedProduct.DetailedProduct.DetailedProductPrice;
                        isAdded = true;
                    }
                }
            }
            return isAdded;
        }
        public bool RemoveDetailedProduct (DetailedProduct product)
        {
            bool isRemoved = false;
            if(ProductList != null)
            {
                if(this.ProductList.ContainsKey(product.ProductId))
                {
                    OrderedDetailedProduct orderedDetailedProduct = this.ProductList[product.ProductId];
                    this.CartTotalPrice -= orderedDetailedProduct.DetailedProduct.DetailedProductPrice * orderedDetailedProduct.Quantity;
                    this.ProductList.Remove(product.ProductId);
                    isRemoved = true;
                }
            }
            return isRemoved;
        }
        public void RemoveCart ()
        {
            this.ProductList = null;
            this.CartTotalPrice = 0;
        }
    }
}
