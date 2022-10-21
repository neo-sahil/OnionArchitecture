using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.DTO.MIddleWare;
using Services.JWT;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.MiddleWare
{
    public class JWTMIddleWare
    {
        private readonly RequestDelegate _next;
        private readonly JWTSettings _jwtSetting;

        public JWTMIddleWare(RequestDelegate next, IOptions<JWTSettings> jwtSetting)
        {
            _next = next;
            _jwtSetting = jwtSetting.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if(token != null)
                await AttachUserToContext(context, token);

            await _next(context);
        }

        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_jwtSetting.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew  = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var username = jwtToken.Claims.First(x => x.Type == "name").Value;

                if(username != null)
                {
                    CurrentUser user = new CurrentUser()
                    {
                        UserName = username,
                    };
                    context.Items["CurrentUser"] = user;    
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
