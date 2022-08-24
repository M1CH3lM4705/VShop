
using System.ComponentModel.DataAnnotations;


namespace VShop.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatorio."),Display(Name = "Nome")]
    public string? Nome { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatorio."),Display(Name = "Preço")]
    public Decimal Price { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatorio."),Display(Name = "Descrição")]
    public string? Description { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatorio."),Display(Name = "Estoque")]
    public long Stock { get; set; }
    [Required(ErrorMessage = "O campo {0} é obrigatorio."), Display(Name = "Imagem")]
    public string? ImageUrl { get; set; }
    public string? CategoryName { get; set; }
    [Range(1, 100)]
    public int Quantity { get; set; } = 1;
    public int CategoryId { get; set; }
}
