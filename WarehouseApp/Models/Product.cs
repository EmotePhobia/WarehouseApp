using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseApp.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        [StringLength(100, ErrorMessage = "Max 100 characters.")]
        public string Name { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "Max 255 characters.")]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        [DataType(DataType.Currency, ErrorMessage = "Only number.")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "int")]
        public int Stock { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
