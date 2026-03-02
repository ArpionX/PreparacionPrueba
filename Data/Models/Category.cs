using System.ComponentModel.DataAnnotations;

namespace Prueba2.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public required string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
