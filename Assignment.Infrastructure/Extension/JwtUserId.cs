using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Extension
{
    public class JwtUserId
    {
        private readonly IConfiguration _configuration;

        public JwtUserId(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // trích xuất token từ header Authorization của request HTTP
        public string ExtractTokenFromHeader(HttpRequest request)
        {
            var authorizationHeader = request.Headers["Authorization"].FirstOrDefault();
            if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            {
                return authorizationHeader.Substring("Bearer ".Length).Trim();
            }
            return null;
        }
        public string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero // Tùy chọn: loại bỏ độ trễ đồng hồ mặc định
                }, out SecurityToken validatedToken);

                var userIdClaim = claimsPrincipal.FindFirst("UserId");
                return userIdClaim?.Value;
            }
            catch
            {
                // Xác thực token không thành công
                return null;
            }
        }
    }
}
