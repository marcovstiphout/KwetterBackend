using Kwetter.Services.AuthService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface ITokenChecker
    {
        Task<ClaimsDTO> VerifyTokenAsync(string jwt);
        Task<bool> AddClaims(string jwt, Dictionary<string, object> claims);
        Task<bool> CheckValidPermissions(string jwt);
    }
}
