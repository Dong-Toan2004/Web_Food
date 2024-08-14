using Assignment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.CartDto
{
    public class CartDetailDto
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public CartDetailStatust Statust { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
