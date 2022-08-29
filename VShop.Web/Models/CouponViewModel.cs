namespace VShop.Web.Models;

    public class CouponViewModel
    {
        public int Id { get; set; }
        public string? CouponCode { get; set; }
        public decimal Discount { get; set; }

        internal bool ExistCoupon()
        {
            return CouponCode is not null;
        }
    }
