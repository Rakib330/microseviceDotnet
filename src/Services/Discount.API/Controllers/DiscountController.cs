using CoreApiResponse;
using Discount.API.Modals;
using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DiscountController : BaseController
    {
        ICouponRepository _couponRepository;
        public DiscountController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }
        [HttpGet]
        [ProducesResponseType(typeof(Coupon),(int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetDiscount(string productId)
        {
            try { 
                var coupon = await _couponRepository.GetDiscount(productId);
                return CustomResult(coupon);
            }
            catch(Exception ex) {
                return CustomResult(ex.Message,System.Net.HttpStatusCode.BadRequest);
            }
        }


        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var IsSaved = await _couponRepository.CreateDiscount(coupon);
                if (IsSaved)
                {
                    return CustomResult("Discount Saved Successfully.",coupon);
                }
                return CustomResult("Discount Saved Failed.",coupon,HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }



        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            try
            {
                var IsSaved = await _couponRepository.UpdateDiscount(coupon);
                if (IsSaved)
                {
                    return CustomResult("Discount Updated Successfully.", coupon);
                }
                return CustomResult("Discount Update Failed.", coupon, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }



        [HttpDelete]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteDiscount(string productId)
        {
            try
            {
                var IsSaved = await _couponRepository.DeleteDiscount(productId);
                if (IsSaved)
                {
                    return CustomResult("Discount Deleted Successfully.");
                }
                return CustomResult("Discount Delete Failed.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
