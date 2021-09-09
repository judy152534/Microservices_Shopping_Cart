﻿using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly ICouponRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;
        
        public DiscountService(ICouponRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetCoupon(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productName {request.ProductName} not found."));
            }
            _logger.LogInformation($"DiscountService retrieving data: productName:{coupon.ProductName}, Amount:{coupon.Amount}, Description:{coupon.Description}");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.CreateCoupon(coupon);
            _logger.LogInformation($"Discount is successfully created. productName:{coupon.ProductName}.");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);
            await _repository.UpdateCoupon(coupon);
            _logger.LogInformation($"Discount update: productName{coupon.ProductName}.");

            var couponModel = _mapper.Map<CouponModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteCoupon(request.ProductName);
            var response = new DeleteDiscountResponse
            {
                Success = deleted,
            };
            return response;
        }
    }
}
