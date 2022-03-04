using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService catalogService;
        private readonly IBasketService basketService;
        private readonly IOrderService orderService;

        public ShoppingController(ICatalogService catalogService, IBasketService basketService, IOrderService orderService)
        {
            this.catalogService = catalogService;
            this.basketService = basketService;
            this.orderService = orderService;
        }

        [HttpGet("{userName}",Name ="GetShopping")]
        [ProducesResponseType(typeof(ShopppingModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShopppingModel>> GetShopping(string userName) 
        {
            BasketModel basket = await basketService.GetBasketByUsername(userName);

            foreach (var item in basket.Items)
            {
                CatalogModel catalog = await catalogService.GetCatalog(item.ProductId);

                item.Category = catalog.Name;
                item.Summary = catalog.Summary;
                item.ImageFile = catalog.ImageFile;
                item.Description = catalog.Description;
            }
            // todo getPrice
            var orders = await orderService.GetOrderByUsername(userName);

            List<BasketItemModel> items = new();


            BasketModel basketModel = new() 
            {
                UserName = userName,
                Items = items,
                
            };

            ShopppingModel viewModel = new() 
            {
                UserName = userName,
                BasketWithProducts = basketModel,
                Orders = orders,
            };

            return Ok(viewModel);

        }
    }
}
