using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SDDEMO.Application.Configuration;
using SDDEMO.Application.Interfaces.UnitOfWork;
using SDDEMO.Domain.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Manager.Helpers
{
    public class TokenProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUnitOfWork unitOfWork;
        private readonly AppConfigData appConfigData;
        private string secretKey = "";

        public TokenProvider()
        {
            this.appConfigData = AppConfigData.GetAppConfig();
            this.secretKey = appConfigData.jwtSecretKey;
        }

        public TokenProvider(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.unitOfWork = unitOfWork;
            this.appConfigData = AppConfigData.GetAppConfig();
            this.secretKey = appConfigData.jwtSecretKey;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
                    {
                        new Claim("Guid", user.id.ToString()),
                        new Claim("Username", user.username),
                        new Claim("IsActive", user.isActive.ToString())
                    };

            var token = new JwtSecurityToken
            (
                issuer: "*",
                audience: "*",
                claims: claims,
                expires: DateTime.Now.AddHours(appConfigData.jwtExpire),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User GetUserByToken()
        {
            try
            {
                var accessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", String.Empty);
                var token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                var userId = token.Claims.First(claim => claim.Type == "Guid").Value;

                return unitOfWork.userRepository.GetById(Guid.Parse(userId));
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
