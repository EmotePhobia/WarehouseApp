using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseApp.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        [StringLength(50, ErrorMessage = "Max 50 characters.")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [StringLength(255, ErrorMessage = "Max 255 characters.")]
        public string Description { get; set; }


        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
