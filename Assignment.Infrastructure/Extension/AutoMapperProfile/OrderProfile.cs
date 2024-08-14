using Assignment.Application.DataTransferObj.OderDetailDto;
using Assignment.Application.DataTransferObj.OrderDto;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.AutoMapperProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<OrderCreateRequest, Order>().ReverseMap();
        }
    }
}
