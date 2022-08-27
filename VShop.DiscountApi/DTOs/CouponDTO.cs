using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VShop.DiscountApi.DTOs;

public class CouponDTO
{
    public int CouponId { get; set; }
    [Required]
    public string? CouponCode { get; set; }
    [Required]
    public decimal Discount { get; set; }
}
