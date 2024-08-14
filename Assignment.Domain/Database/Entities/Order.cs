using Assignment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
