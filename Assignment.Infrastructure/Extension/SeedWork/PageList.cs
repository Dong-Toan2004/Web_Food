using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.SeedWork
{
    public class PageList<T> // danh sách trang
    {
        public MetaData MetaData { get; set; }//Thuộc tính MetaData lưu trữ thông tin về siêu dữ liệu của trang 
        public List<T> Items { set; get; }//Thuộc tính Items lưu trữ danh sách các mục trong trang hiện tại (Lấy dữ liệu từ db)

        public PageList() { }//Đây là constructor mặc định không có tham số.Nó khởi tạo một đối tượng PagedList mà không thiết lập bất kỳ thuộc tính nào.
        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,//Tổng số bản ghi trong toàn bộ tập dữ liệu.
                PageSize = pageSize,// Số trang hiện tại.
                CurrentPage = pageNumber,//Kích thước trang.
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
            Items = items; //Danh sách các mục thuộc kiểu T để lưu trữ trong Items.
        }
    }
}
