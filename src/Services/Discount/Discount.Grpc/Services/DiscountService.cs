﻿using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Object Argument")); 

            dbContext.Add(coupon);
           await dbContext.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();

        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName == request.ProductName);

            if (coupon == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Object Argument"));

            dbContext.Remove(coupon);
            await dbContext.SaveChangesAsync();

            return new DeleteDiscountResponse { Success = true};
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(w => w.ProductName == request.ProductName);

            if (coupon is null)
            {
                coupon = new Coupon
                {
                    ProductName = "No Discount",
                    Amount = 0,
                    Description = string.Empty,
                    Id = 0,
                };

            }

            logger.LogInformation("Discount is retrieved for ProductName {productName}", request.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;

        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon == null) throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Object Argument"));

            dbContext.Update(coupon);
            await dbContext.SaveChangesAsync();

            return coupon.Adapt<CouponModel>();
        }
    }
}
