using Assignment.Application.DataTransferObj.ProductDto;
using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension.AutoMapperProfile
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<UserCreateRequest, User>();
        }
    }
}
