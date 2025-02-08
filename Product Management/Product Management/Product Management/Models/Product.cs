using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Product_Management.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Category? Category { get; set; } 
    }

}
