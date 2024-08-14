using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.AutoMapperProfile
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<CategoryCreateRequest,Category>();
        }
    }
}
