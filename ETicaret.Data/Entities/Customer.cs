using Microsoft.Build.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ETicaret.Data.Entities
{
    public class Customer : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string Surname { get; set; }
        [Required]
        [StringLength(60)]
        public string EMail { get; set; }
        [Required]
        [StringLength(20)]
        public string Phone { get; set; }
        [Required]
        [StringLength(60)]

        public string City { get; set; }
        [Required]
        [StringLength(100)]
        public string Country { get; set; }
        [Required]
        [StringLength(200)]
        public string Adress { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }
        public string PwdKey { get; set; }

        [Required]
        public byte[] Picture { get; set; }

        public List<Basket> Baskets { get; set; } = new List<Basket>();


    }
}
