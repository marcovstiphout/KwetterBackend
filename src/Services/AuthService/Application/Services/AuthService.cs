using Domain;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthHttpRequest _authHttpRequest;
        private readonly IAuthContext _authContext;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AuthService(IAuthHttpRequest authHttpRequest, IAuthContext authContext)
        {
            _authHttpRequest = authHttpRequest;
            _authContext = authContext;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); ;
        }
        public async Task<AuthResponseDto> AuthorizeAsync(string code)
        {
            AuthResponseDto response = await _authHttpRequest.SendAuthRequest(code);
            if (true)
            {
                JwtSecurityToken token = _jwtSecurityTokenHandler.ReadJwtToken(response.IdToken);
                string openId = token.Subject;
                User user = _authContext.Users.FirstOrDefault(x => x.OpenId == openId);

                if (user == null)
                {
                    user = new User
                    {
                        Id = new Guid(),
                        Avatar = token.Claims.First(x => x.Type == "picture").Value,
                        Name = token.Claims.First(x => x.Type == "given_name").Value,
                        OpenId = openId
                    };
                    _authContext.Users.Add(user);
                    await _authContext.SaveChangesAsync();
                }

                response.UserId = user.Id;

            }
            return response;
        }
    }
}
