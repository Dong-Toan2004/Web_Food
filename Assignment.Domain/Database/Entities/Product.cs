using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Image {  get; set; }
        [Required]
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string? QRCode { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ComboDetail> ComboDetails { get; set; }
        public virtual ICollection<Cartdetail> Cartdetails { get; set; }
        public virtual Category Category { get; set; }
    }
}
