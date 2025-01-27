﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Base
{
    internal interface IDeletedBase
    {
        public bool Deleted { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTimeOffset DeletedTime { get; set; }
    }
}
