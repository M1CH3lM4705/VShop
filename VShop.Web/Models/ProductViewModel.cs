using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    public int CategoryId { get; set; }
}
