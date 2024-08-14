using Assignment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.OrderDto
{
    public class OrderDto
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
    }
}
