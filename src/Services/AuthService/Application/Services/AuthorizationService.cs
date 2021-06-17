using Domain;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Interfaces.Services;
using Kwetter.Services.AuthService.Application.Common.Models;
using Kwetter.Services.Shared.Messaging.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Services
{
    public class AuthorizationService : IAuthoService
    {
        private readonly IAuthContext _authContext;
        private readonly ILogger<AuthService> _logger;
        private readonly ITokenChecker _tokenChecker;
        private readonly IMessagePublisher _publisher;
        public AuthorizationService( IAuthContext authContext, ITokenChecker tokenChecker, ILogger<AuthService> logger, IMessagePublisher publisher)
        {
            _authContext = authContext;
            _logger = logger;
            _tokenChecker = tokenChecker;
            _publisher = publisher;
        }
        public async Task<UserDTO> SetUserClaims(string uid)
        {
            var response = new UserDTO();

            try
            {
                var claimsDto = await _tokenChecker.VerifyTokenAsync(uid);

                if (claimsDto == null) return response;

                var userExist = await _authContext.Users.FirstOrDefaultAsync(x =>
                    x.LoginProviderId == claimsDto.Claims["user_id"].ToString());
                if (userExist == null)
                {
                    var user = new User
                    {
                        Id = Guid.NewGuid(),
                        Name = claimsDto.Claims["name"].ToString(),
                        Avatar = claimsDto.Claims["picture"].ToString(),
                        LoginProviderId = claimsDto.Claims["user_id"].ToString(),
                        Email = claimsDto.Claims["email"].ToString()
                    };

                    _authContext.Users.Add(user);
                    await _authContext.SaveChangesAsync();

                    await NewUserRegistered(user);

                    var claims = new Dictionary<string, object>
                    {
                        {"Id", user.Id},
                        {"user", true},
                        {"Role","User" }
                    };
                    await _tokenChecker.AddClaims(claimsDto.Subject, claims);

                    _logger.LogInformation("Set user claims of user: " + user.Id);
                    return user;
                }
            }
            catch (FirebaseAuthException)
            {
                _logger.LogError("Claims could not be added to user!");
                throw;
            }
            catch (ArgumentNullException)
            {
                _logger.LogError("Claims could not be added to user!");
                throw;
            }

            return response;
        }
        private async Task NewUserRegistered(UserDTO userDTO)
        {
            await _publisher.PublishMessageAsync<UserDTO>("UserCreatedEvent", userDTO);
        }
        public async Task<bool> AssignElevatedPermissions(string assigningModeratorUid, Guid userToElevate, string roleToAssign)
        {
            if(await _tokenChecker.CheckValidPermissions(assigningModeratorUid))
            {
                var user = await _authContext.Users.FirstOrDefaultAsync(x => x.Id == userToElevate);
                var claims = new Dictionary<string, object>
                {
                    {"Id", user.Id},
                    {"Role", roleToAssign}
                };
                return await _tokenChecker.AddClaims(user.LoginProviderId, claims);
            }
            return false;
        }
    }
}
