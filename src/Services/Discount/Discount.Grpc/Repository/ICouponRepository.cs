﻿using Discount.Grpc.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repository
{
    public interface ICouponRepository
    {
        Task<Coupon> GetCoupon(string productName);
        Task<bool> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(string productName);

    }
}
