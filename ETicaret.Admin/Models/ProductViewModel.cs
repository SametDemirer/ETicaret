using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ETicaret.Admin.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Ürün adı maksimum 50 karakter olabilir.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Ürün açıklaması alanı doldurulmak zorundadır.")]
        [DisplayName("Product Description")]
        public string Description { get; set; }
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        [DisplayName("Unit In Stock")]
        public int UnitInStock { get; set; }

        [DisplayName("Product Picture")]
        public IFormFile Picture { get; set; }
        public string ImageString { get; set; }
        public byte[] ImageByteArray { get; set; }
        public int CategoryId { get; set; }
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}
