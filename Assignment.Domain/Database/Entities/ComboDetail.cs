using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Entities
{
    public class ComboDetail
    {
        public Guid Id { get; set; }
        public Guid ComboId { get; set; }
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
        public virtual Combo Combo { get; set; }
    }
}
