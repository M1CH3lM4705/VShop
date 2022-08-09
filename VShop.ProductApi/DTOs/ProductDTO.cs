using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VShop.ProductApi.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é requerido"), MinLength(3), MaxLength(100)]

        public string? Nome { get; set; }
        [Required(ErrorMessage = "O Preço é requirido.")]
        public Decimal Price { get; set; }

        [Required(ErrorMessage = "O Nome é requerido"), MinLength(5), MaxLength(200)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "O Estoque é requirido.")]
        [Range(1, 9999, ErrorMessage = "Tamanho mínimo é de {0} e máximo é de {1}")]
        public long Stock { get; set; }
        public string? ImageUrl { get; set; }

        public string? CategoryName { get; set; }
        [JsonIgnore]
        public CategoryDTO? Category { get; set; }
        public int CategoryId { get; set; }
    }
}