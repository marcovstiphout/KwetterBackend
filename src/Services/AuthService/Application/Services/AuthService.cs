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
                User user = _authContext.Users.FirstOrDefault(x => x.LoginProviderId == openId);

                if (user == null)
                {
                    user = new User
                    {
                        Id = new Guid(),
                        Avatar = token.Claims.First(x => x.Type == "picture").Value,
                        Name = token.Claims.First(x => x.Type == "given_name").Value,
                        LoginProviderId = openId
                    };
                    _authContext.Users.Add(user);
                    await _authContext.SaveChangesAsync();
                }

                response.UserId = user.Id;

            }
            return response;
        }

        public async Task<bool> CheckAccountExistsAsync(string email)
        {
            return _authContext.Users.FirstOrDefault(x => x.Email == email) != null;
        }

        public async Task<bool> CreateAccountAsync(string email, string name)
        {
            User newUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Email = email               
            };
            await _authContext.Users.AddAsync(newUser);
            bool success = await _authContext.SaveChangesAsync() > 0;

            if (!success)
            {
                throw new InvalidOperationException("Something went wrong trying to create the account");
            }
            return true;
        }
    }
}
