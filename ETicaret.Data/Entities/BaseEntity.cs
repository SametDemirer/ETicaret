using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaret.Data.Entities
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreatedDate { get; set; }
        int CreatedById { get; set; }
        User CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        int? UpdatedById { get; set; }
        User UpdatedBy { get; set; }
        bool IsActive { get; set; }
    }
    public abstract class BaseEntity : IBaseEntity
    {
        
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public User CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedById { get; set; }

        [ForeignKey("UpdatedById")]
        public User UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
