using System.ComponentModel.DataAnnotations;

namespace VShop.Cart.DTOs;

public class CartHeaderDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Usuário é obrigatorio")]
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
}
