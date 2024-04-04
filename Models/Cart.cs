using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace DreamerStore2.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }
        public decimal CartTotalPrice { get; set; }
        public int TotalQuantity { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
            CartTotalPrice = 0;
            TotalQuantity = 0;
        }

        public bool AddToCart(DetailedProduct detailedProduct, int quantity)
        {
            CartItem existingItem = Items.FirstOrDefault(item => item.DetailedProduct.DetailedProductId == detailedProduct.DetailedProductId);
            if (existingItem != null)
            {
                if(detailedProduct.DetailedProductQuantity >= existingItem.Quantity + quantity)
                {
                    existingItem.Quantity += quantity;
                    TotalQuantity += quantity;
                    CartTotalPrice += detailedProduct.DetailedProductPrice * quantity;
                    return true;
                } else
                {
                    return false;
                }
            }
            else
            {
                Items.Add(new CartItem(detailedProduct, quantity));
                TotalQuantity += quantity;
                CartTotalPrice += detailedProduct.DetailedProductPrice * quantity;
                return true;
            }
        }


        public bool RemoveFromCart(int detailedProductId, int quantity)
        {
            CartItem existingItem = Items.FirstOrDefault(item => item.DetailedProduct.DetailedProductId == detailedProductId);
            if(existingItem != null)
            {
                existingItem.Quantity -= quantity;
                TotalQuantity -= quantity;
                CartTotalPrice -= existingItem.DetailedProduct.DetailedProductPrice * quantity;
                if (existingItem.Quantity == 0)
                {
                    Items.Remove(existingItem);
                }
                return true;
            }
            return false;
        }

        public void RemoveAll()
        {
            Items.Clear();
            CartTotalPrice = 0;
            TotalQuantity = 0;
        }

        public bool IsEmpty()
        {
            return (Items.Count == 0) ? true : false;
        }
    }
}
