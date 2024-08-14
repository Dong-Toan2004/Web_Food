using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.AutoMapperProfile
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartDto, Cart>().ReverseMap();
            CreateMap<CartDetailDto, Cartdetail>().ReverseMap();
            CreateMap<CartDetailCreateRequest, Cartdetail>().ReverseMap();
        }
    }
}
