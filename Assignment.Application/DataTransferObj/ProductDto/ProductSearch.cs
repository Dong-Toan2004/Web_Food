using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.ProductDto
{
    public class ProductSearch
    {
        public string? Name { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
