namespace VShop.Web.Models;

public class CartHeaderViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string CouponCode { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; } = 0.00m;
    public decimal Discount { get; set; } = 0.00m;
    public ClienteViewModel Cliente { get; set; } = new();
    public CreditCard Card { get; set; } = new();

    internal void GetTotalDiscount(){
        TotalAmount = TotalAmount - (TotalAmount * Discount) / 100;
    }

    internal bool CupomEstaVazio()
    {
        return !string.IsNullOrEmpty(CouponCode);
    }
}
