using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WarehouseApp.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime OrderDate { get; set; } = DateTime.Now;


        [Required]
        [Column(TypeName = "varchar(15)")]
        public string Status { get; set; } // "Pending", "Completed", "Cancelled"


        public enum OrderStatus
        {
            Pending,
            Completed,
            Cancelled
        }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }
        public Customer IdentityUser { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
    }
}
