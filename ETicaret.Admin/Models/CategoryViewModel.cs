using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ETicaret.Admin.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        [DisplayName("Category Description")]
        public string Description { get; set; }

        [DisplayName("Category Picture")]
        public IFormFile Picture { get; set; }
        public string ImageString { get; set; }
        public byte[] ImageByteArray { get; set; }
    }
}
