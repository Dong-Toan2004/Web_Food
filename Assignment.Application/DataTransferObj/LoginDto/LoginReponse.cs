﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.LoginDto
{
    public class LoginReponse
    {
        public bool Successfull { get; set; }
        public string Error { get; set; }
        public string Token { get; set; }
    }
}
