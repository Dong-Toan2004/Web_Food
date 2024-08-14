using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.OrderDto
{
    public class OrderCreateRequest
    {
        [Required]
        public string ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
    }
}
