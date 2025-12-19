using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TrueCounsel.Application.Common.Models;

namespace TrueCounsel.Application.Features.Auth.Dtos
{
    public record LoginResponseDto
    {
        public string AccessToken { get; init; } = null!;
        public string RefreshToken { get; init; } = null!;
        public string TokenType { get; init; } = "Bearer";
        public int ExpiresIn { get; init; }
        public UserDto User { get; init; } = null!;
    }


}
