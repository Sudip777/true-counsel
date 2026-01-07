using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Domain.Common
{
    // why abstraact: This is a base class. Don’t create objects of this type. Only inherit it.
    // can't be instantiated so only to be inherited
    public abstract class BaseAuditableEntity:BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }  // For soft delete
    }
}
