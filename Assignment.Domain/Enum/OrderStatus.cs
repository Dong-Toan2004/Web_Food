using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Enum
{
    public enum OrderStatus
    {
        Pending,// Đang chờ
        Processing,// Đang xử lý
        Shipping,// Đang giao hàng
        Delivered,// Đã giao hàng
        Cancelled // Đã hủy
    }
}
