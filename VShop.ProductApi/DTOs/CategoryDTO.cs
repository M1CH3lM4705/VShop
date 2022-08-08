using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VShop.ProductApi.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "O Nome é requerido"), MinLength(3), MaxLength(100)]
        public string? Name { get; set; }
        public ICollection<ProductDTO>? Products {get; set;}
    }
}