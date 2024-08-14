using Assignment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Entities
{
    public class Cartdetail
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public CartDetailStatust Statust { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
