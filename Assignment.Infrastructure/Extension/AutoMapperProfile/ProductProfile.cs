using Assignment.Application.DataTransferObj.ProductDto;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.AutoMapperProfile
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<ProductCreateRequest, Product>();
        }
    }
}
