using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ETicaret.UI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
       
        public string ProductName { get; set; }

        public string Description { get; set; }

        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        
        public int UnitInStock { get; set; }

        public IFormFile Picture { get; set; }

        public string ImageString { get; set; }

        public byte[] ImageByteArray { get; set; }

        public int CategoryId { get; set; }

    }
}
