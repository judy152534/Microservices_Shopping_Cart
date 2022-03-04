using System.Collections.Generic;

namespace Shopping.Aggregator.Models
{
    public class ShopppingModel
    {
        public string UserName { get; set; }
        public BasketModel BasketWithProducts { get; set; }
        public IEnumerable<OrderResponseModel> Orders { get; set; }
    }
}
