using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Application.Features.Auth.Dtos
{
    public record AuthUserDto(
     int Id,
     string Email,
     string Name,
     string Role
 );

}
