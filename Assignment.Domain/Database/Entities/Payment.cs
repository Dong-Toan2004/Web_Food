using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public decimal Amount {  get; set; }
        public int PaymentStatus { get; set; }
        public string QrCode { get; set; }
        public virtual Order Order { get; set; }
    }
}
