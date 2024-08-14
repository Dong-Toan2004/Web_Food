using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.SeedWork
{
    public class MetaData
    {
        public int CurrentPage { get; set; }//Trang hiện tại
        public int TotalPages { get; set; }//Tổng số trang
        public int PageSize { get; set; }// Kích thước size
        public int TotalCount { get; set; }// Tổng số
        public bool HasPrevious => CurrentPage > 1; //HasPrevious: đã có trước đó
        public bool HasNext => CurrentPage < TotalPages;
    }
}
