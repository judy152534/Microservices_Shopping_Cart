using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<bool> CreateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCoupon(string productName)
        {
            throw new NotImplementedException();
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var sql = @"select ProductName, Description, Amount from Coupon where ProductName = @ProductName;";
            var coupon = await connection.QuerySingleOrDefaultAsync<Coupon>(sql, new { ProductName = productName });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount desc" };
            
            return coupon;
        }

        public Task<bool> UpdateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }
    }
}
