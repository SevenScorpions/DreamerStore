using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace DreamerStore2.Models
{
    public class Cart
    {
        public Dictionary<int, List<OrderedDetailedProduct>> ProductList { get; set; }
        public decimal CartTotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public Cart()
        {
            ProductList = new Dictionary<int, List<OrderedDetailedProduct>>();
            CartTotalPrice = 0;
            TotalQuantity = 0;
        }

        public Cart(Dictionary<int, List<OrderedDetailedProduct>> productList, decimal cartTotalPrice, int totalQuantity)
        {
            ProductList = productList;
            CartTotalPrice = cartTotalPrice;
            TotalQuantity = totalQuantity;
        }

        //add theo mã chi tiết sản phẩm
        public bool AddToCart(DetailedProduct dProduct, Cart cart, int buyingQuantity)
        {
            if (dProduct.DetailedProductQuantity < buyingQuantity)
                return false;

            var productId = dProduct.ProductId;
            var detailedProductId = dProduct.DetailedProductId;
            var detailedProductPrice = dProduct.DetailedProductPrice;
            var detailedProductQuantity = dProduct.DetailedProductQuantity;

            if (!cart.ProductList.TryGetValue(productId, out var listDetailedProduct))
            {
                listDetailedProduct = new List<OrderedDetailedProduct>();
                cart.ProductList.Add(productId, listDetailedProduct);
            }

            var detailedProductInCart = listDetailedProduct.FirstOrDefault(item => item.DetailedProduct.DetailedProductId == detailedProductId);

            if (detailedProductInCart != null)
            {
                var newQuantity = detailedProductInCart.Quantity + buyingQuantity;
                if (newQuantity <= detailedProductQuantity && detailedProductQuantity > 0)
                {
                    detailedProductInCart.Quantity = newQuantity;
                    cart.CartTotalPrice += detailedProductPrice * buyingQuantity;
                    cart.TotalQuantity += buyingQuantity;
                    return true;
                }
                return false;
            }

            if (detailedProductQuantity >= buyingQuantity && detailedProductQuantity > 0)
            {
                listDetailedProduct.Add(new OrderedDetailedProduct(dProduct, buyingQuantity));
                cart.CartTotalPrice += detailedProductPrice * buyingQuantity;
                cart.TotalQuantity += buyingQuantity;
                return true;
            }

            return false;
        }

        /*public async Task<IActionResult> RemoveDetailedProduct(int id)
        {
            var cartKey = HttpContext.Session.GetString("Cart_" + HttpContext.Session.Id);
            Cart cartObj = JsonConvert.DeserializeObject<Cart>(cartKey);
            if (cartObj != null)
            {
                DetailedProduct product = _context.DetailedProducts.FirstOrDefault(p => p.DetailedProductId == id);
                if (cartObj.ProductList.ContainsKey(product.ProductId))
                {
                    OrderedDetailedProduct orderedDetailedProduct = cartObj.ProductList[product.ProductId];
                    cartObj.CartTotalPrice -= orderedDetailedProduct.DetailedProduct.DetailedProductPrice * orderedDetailedProduct.Quantity;
                    cartObj.ProductList.Remove(product.ProductId);
                }
                cartKey = JsonConvert.SerializeObject(cartObj);
                HttpContext.Session.SetString("Cart_" + HttpContext.Session.Id, cartKey);
            }
            return RedirectToAction("Index");
        }*/

        public bool RemoveItemInCart (DetailedProduct dProduct, Cart cart)
        {
            var productId = dProduct.ProductId;
            if (cart.ProductList.ContainsKey(productId))
            {
                var listDetailedProduct = cart.ProductList[productId];
                OrderedDetailedProduct itemToRemove = listDetailedProduct.FirstOrDefault(item => item.DetailedProduct.DetailedProductId == dProduct.DetailedProductId);
                cart.ProductList[productId].Remove(itemToRemove);
                cart.CartTotalPrice -= itemToRemove.DetailedProduct.DetailedProductPrice * itemToRemove.Quantity;
                cart.TotalQuantity -= itemToRemove.Quantity;

                if (cart.ProductList[productId].Count == 0)
                {
                    cart.ProductList.Remove(productId);
                }
                return true;
            }
            return false;
        }
        public bool isEmpty(Cart cart)
        {
            return (cart.ProductList.Count == 0) ? true : false;
        }
        public void RemoveCart(HttpContext context)
        {
            context.Session.Remove("Cart_" + context.Session.Id);
        }
    }
}
