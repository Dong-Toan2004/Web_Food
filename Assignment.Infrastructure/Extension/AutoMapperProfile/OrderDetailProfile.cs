using Assignment.Application.DataTransferObj.OderDetailDto;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.AutoMapperProfile
{
    public class OrderDetailProfile : Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailCreateRequest, OrderDetail>().ReverseMap();
        }
    }
}
