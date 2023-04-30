using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [StringLength(50, ErrorMessage = "Ürün adı maksimum 50 karakter olabilir.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Ürün açıklaması alanı doldurulmak zorundadır.")]
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int UnitInStock { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        #region Relations
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public List<BasketItem> BasketItems { get; set; }
        #endregion


    }
}
