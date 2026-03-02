using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Prueba2.Data.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public required string Name { get; set; } = null!;
        public required string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 999999999.99, ErrorMessage = "Price must be greater than 0")]
        public required decimal Price { get; set; } = 0;
        [Range(0, int.MaxValue, ErrorMessage = "Stock no debe ser negativo")]
        public int stock { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        //foreign key
        public Guid CategoryId { get; set; }
        public Category Category { get; set; } = null!;

    }
}
