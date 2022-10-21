using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.JWT
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSetting;

        public TokenService(IOptions<JWTSettings> jwtSetting)
        {
            _jwtSetting = jwtSetting.Value;
        }
        async Task<string> ITokenService.CreateToken(AppUser obj)
        {
            var cliams = new List<Claim>
            {
                new Claim(ClaimTypes.Name, obj.UserName),
                //new Claim(ClaimTypes.Role, "Admin")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(cliams),
                Expires = DateTime.Now.AddHours(_jwtSetting.TokenExpirationInHours),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescripter);

            return tokenHandler.WriteToken(token);
            throw new NotImplementedException();
        }
    }
}
