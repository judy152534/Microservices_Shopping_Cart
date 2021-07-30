using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext: ICatalogContext
    {
        public CatalogContext(IConfiguration configuraton)
        {
            var client = new MongoClient(configuraton.GetValue<string>("DatabaseSettings:ConnectionString"));
            var databse = client.GetDatabase(configuraton.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = databse.GetCollection<Product>("DatabaseSettings:CollectionName");
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
