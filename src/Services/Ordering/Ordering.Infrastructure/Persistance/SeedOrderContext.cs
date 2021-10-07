using Microsoft.Extensions.Logging;
using Ordering.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistance
{
    public class SeedOrderContext
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<SeedOrderContext> logger) 
        {
            if (!orderContext.Orders.Any()) 
            {
                await orderContext.AddRangeAsync(GetPreConfigData());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Successfully seed data with DbContext{DbContextName}", typeof(OrderContext));
            }
        }

        public static IEnumerable<Order> GetPreConfigData() 
        {
            return new List<Order>()
            {
                new Order { UserName = "Rain" , FirstName = "Handsome", LastName = "Guy",
                EmailAddress = "handsomeguy@gmail.com", AddressLine = "Noob", Country ="Handsome", TotalPrice = 1850}
            };
        } 
    }
}
