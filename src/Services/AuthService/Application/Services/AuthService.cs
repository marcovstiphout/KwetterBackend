using Domain;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.Shared.Messaging.Interfaces;
using Microsoft.AspNetCore.Authentication;
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
        private readonly IMessagePublisher _publisher;
        private readonly IAuthContext _authContext;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AuthService(IMessagePublisher publisher, IAuthContext authContext)
        {
            _publisher = publisher;
            _authContext = authContext;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler(); ;
        }
        public async Task<bool> AuthorizeAsync(AuthenticateResult authResult)
        {
          //  AuthResponseDto response = await _authHttpRequest.SendAuthRequest(code);
            var claims = authResult.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            var email = claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").Value;
            var name = claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value; 
            //Check if Account already exists, and register one if this is not the case. Afterwards generate a JWT
            if (!CheckAccountExistsAsync(email).Result)
            {
                await CreateAccountAsync(email, name);
            }
            
            return true;
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
