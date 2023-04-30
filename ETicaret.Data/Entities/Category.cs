using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public byte[] Image { get; set; }
        public List<Product> Products { get; set; }

    }
}
