using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Entities
{
    public class ShoppingCart
    {
        public string UserName { get; set; }
        public List<ShoppingItem> Items { get; set; } = new List<ShoppingItem>();
        public ShoppingCart()
        {
        }
        public ShoppingCart(string username)
        {
            UserName = username;
        }

        public decimal TotalPrice 
        {
            get 
            {
                decimal totalPrice = 0;
                foreach (var item in Items) 
                {
                    totalPrice += item.Price * item.Quantity;
                }
                return totalPrice;
            }
        }
    }
}
