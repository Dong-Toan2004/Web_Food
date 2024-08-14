using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Domain.Database.Base
{
    public class EntityBase : ICreatedBase, IModifiedBase, IDeletedBase
    {
        public DateTimeOffset CreatedTime { get; set; }//Thời gian thực hiện
        public Guid? CreatedBy { get; set; }//Id người thực hiện
        public DateTimeOffset ModifiedTime { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool Deleted { get; set; }
        public Guid? DeletedBy { get; set; }
        public DateTimeOffset DeletedTime { get; set; }
    }
}
