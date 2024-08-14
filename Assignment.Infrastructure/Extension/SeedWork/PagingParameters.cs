using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.SeedWork
{
    public class PagingParameters
    {
        const int maxPageSize = 50;//Đây là một hằng số xác định kích thước trang tối đa là 50
        public int PageNumber { get; set; } = 1; //Thuộc tính PageNumber đại diện cho số trang hiện tại
        private int _pageSize = 5; //Biến riêng _pageSize được sử dụng để lưu trữ kích thước của trang.Nó được khởi tạo với giá trị mặc định là 4.(Tráng có thể lưu được 4 giá trị)
        public int PageSize // Nếu PageSize quá 50 giữ liệu ở trong thì sẽ chuyển về maxPageSize = 50
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
