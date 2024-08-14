﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.DataTransferObj.CategoryDto
{
    public class CategoryUpdateRequest
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
