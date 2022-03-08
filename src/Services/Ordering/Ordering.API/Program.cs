using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ordering.API.Extension;
using Ordering.Infrastructure.Persistance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ordering.Application;
using MassTransit;
using Ordering.API.EventBusConsumer;
using EventBus.Messages.Common;
using Ordering.API;

var builder = WebApplication.CreateBuilder(args);

var startUp = new Startup(builder.Configuration);

startUp.ConfigureServices(builder.Services);

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, service) =>
{
    var logger = service.GetService<ILogger<SeedOrderContext>>();
    SeedOrderContext.SeedAsync(context, logger).Wait();
});

startUp.Configure(app, app.Environment);

app.Run();

//namespace Ordering.API
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var host = CreateHostBuilder(args).Build();

//            // pass ActionType into method
//            host.MigrateDatabase<OrderContext>((context, service) =>
//            {
//                var logger = service.GetService<ILogger<SeedOrderContext>>();
//                SeedOrderContext.SeedAsync(context, logger).Wait();
//            })
//            .Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder =>
//                {
//                    webBuilder.UseStartup<Startup>();
//                });
//    }
//}
