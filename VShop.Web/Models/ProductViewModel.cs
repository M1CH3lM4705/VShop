using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VShop.Web.Models;

public class ProductViewModel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public Decimal Price { get; set; }
    public string? Description { get; set; }
    public long Stock { get; set; }
    public string? ImageUrl { get; set; }
    public string? CategoryName { get; set; }
    public int CategoryId { get; set; }
}
