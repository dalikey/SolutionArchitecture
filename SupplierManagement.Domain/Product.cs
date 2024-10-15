using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierManagement.Domain
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        [NotMapped]
        public string ProductDescription { get; set; }

        [NotMapped]
        [Range(1, int.MaxValue, ErrorMessage = "The Price must not be free.")]
        public int? Price { get; set; }

        [NotMapped]
        [Range(1, int.MaxValue, ErrorMessage = "The StockQuantity must not be free")]
        public int? StockQuantity { get; set; }

        [NotMapped]
        public string Category { get; set; }

        [NotMapped]
        public int? SupplierId { get; set; }

        [NotMapped]
        // Navigation property Only for EF Core
        public Supplier? Supplier { get; set; }
    }
}
