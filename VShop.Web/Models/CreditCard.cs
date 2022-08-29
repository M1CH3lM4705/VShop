namespace VShop.Web.Models;

public class CreditCard
{
    public string CardNumber { get; set; } = string.Empty;
    public string NameOnCard { get; set; } = string.Empty;
    public string CVV { get; set; } = string.Empty;
    public string ExpireMonthYear { get; set; } = string.Empty;
}
