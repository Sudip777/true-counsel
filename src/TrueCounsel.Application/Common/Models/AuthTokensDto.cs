using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueCounsel.Application.Common.Models
{
    public class AuthTokensDto
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public string TokenType { get; set; } = "Bearer";
        public int ExpiresIn { get; set; }
        public int RefreshExpiresIn { get; set; }
    }

}
