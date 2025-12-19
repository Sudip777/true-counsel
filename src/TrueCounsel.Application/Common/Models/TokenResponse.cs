using System;

namespace TrueCounsel.Application.Common.Models
{
    public class TokenResponse
    {
       
            public string AccessToken { get; set; } = null!;
            public string RefreshToken { get; set; } = null!;
            public DateTime AccessTokenExpiryTime { get; set; }
            public DateTime RefreshTokenExpiryTime { get; set; }
        

    }
}