using Assignment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.OrderDto
{
    public class OrderUpdateRequest
    {
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
    }
}
