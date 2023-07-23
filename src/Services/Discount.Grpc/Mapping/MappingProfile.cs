using AutoMapper;
using Discount.Grpc.Modals;
using Discount.Grpc.Protos;

namespace Discount.Grpc.MappingProfile
{
    public class MappingProfile:Profile
    {
        public MappingProfile() { 
        CreateMap<Coupon,CouponRequest>().ReverseMap();
        }
    }
}
