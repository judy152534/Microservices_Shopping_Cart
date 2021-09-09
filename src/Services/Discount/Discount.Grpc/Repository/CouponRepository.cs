using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var sql = @"Insert into Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount);";
            int outcome = await connection.ExecuteAsync(sql, coupon);
            if (outcome == 0) 
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteCoupon(string productName)
        {
            using var connection = new NpgsqlConnection
              (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var sql = @"DELETE FROM Coupon WHERE ProductName = @ProductName;";
            var outcome = await connection.ExecuteAsync(sql, new { ProductName = productName });
            if (outcome == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetCoupon(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var sql = @"Select ProductName, Description, Amount from Coupon where ProductName = @ProductName;";
            var coupon = await connection.QuerySingleOrDefaultAsync<Coupon>(sql, new { ProductName = productName });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount desc" };
            
            return coupon;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
               (_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var sql = @"Update Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id;";
            var outcome = await connection.ExecuteAsync(sql, coupon);
            if (outcome == 0)
            {
                return false;
            }
            return true;
        }
    }
}
