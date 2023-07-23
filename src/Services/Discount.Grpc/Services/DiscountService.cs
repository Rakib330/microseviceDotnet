using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService:DiscountProtoService.DiscountProtoServiceBase
    {
        CouponRepository _couponRepository;
        Logger<DiscountService> _logger;
        IMapper _mapper;
        public DiscountService(CouponRepository couponRepository, Logger<DiscountService> logger,IMapper mapper)
        {
            _couponRepository = couponRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponRequest> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _couponRepository.GetDiscount(request.ProductId);

            if(coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount Not Found"));
            }
            _logger.LogInformation("Discount is retrived for ProductName : {productName}, Amount :{Amount}", coupon.ProductName, coupon.Amount);
            //return new CouponRequest { ProductId = coupon.ProductId,ProductName = coupon.ProductName,Description = coupon.Description,Amount=coupon.Amount};
            return _mapper.Map<CouponRequest>(coupon);
        }
    }
}
