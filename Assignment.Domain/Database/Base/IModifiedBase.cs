using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Base
{
    internal interface IModifiedBase
    {
        public DateTimeOffset ModifiedTime { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
