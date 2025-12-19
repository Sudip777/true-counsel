using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Application.Common.Abstractions
{ 
        public interface IClaimsInterface
        {
            public string? Username { get; }
            public string? Role { get; }
            public string? UserId { get; }
        }
    
}
