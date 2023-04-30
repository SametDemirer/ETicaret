using ETicaret.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]

        public string Surname { get; set; }

        [Required]
        [StringLength(100)]
        public string EMail { get; set; }

        [Required]
        [StringLength(12)]
        public string Password { get; set; }

        public Role Role { get; set; }

        public string PwdKey { get; set; }

        [Required]
        public byte[] Picture { get; set; }
    }
}
