using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Infrastructure
{
    public class FirebaseVerifier : ITokenChecker
    {
        private readonly FirebaseAuth _firebaseApp;

        public FirebaseVerifier(FirebaseApp firebaseApp)
        {
            _firebaseApp = _firebaseApp = FirebaseAuth.GetAuth(firebaseApp);
        }
        public async Task<bool> AddClaims(string jwt, Dictionary<string, object> claims)
        {
            await _firebaseApp.SetCustomUserClaimsAsync(jwt, claims);
            return true;
        }

        public async Task<ClaimsDTO> VerifyTokenAsync(string jwt)
        {
            try
            {
                var token = await _firebaseApp.VerifyIdTokenAsync(jwt);

                var claimsDto = new ClaimsDTO
                {
                    Subject = token.Subject,
                    Audience = token.Audience,
                    Issuer = token.Issuer,
                    ExpirationTimeSeconds = token.ExpirationTimeSeconds,
                    IssuedAtTimeSeconds = token.IssuedAtTimeSeconds,
                    Claims = token.Claims
                };

                return claimsDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public async Task<bool> CheckValidPermissions(string jwt)
        {
            try
            {
                var token = await _firebaseApp.VerifyIdTokenAsync(jwt);

                UserRecord user = await FirebaseAuth.DefaultInstance.GetUserAsync(token.Uid);
                if(user.CustomClaims["Role"] is "Admin" or "Moderator")
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
