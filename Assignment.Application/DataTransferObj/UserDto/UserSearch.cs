using Assignment.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.UserDto
{
    public class UserSearch
    {
        public string? UserName { get; set; }
        public Role? Role { get; set; }
    }
}
