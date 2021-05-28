using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<bool> AuthorizeAsync(AuthenticateResult authResult);
        Task<bool> CheckAccountExistsAsync(string email);
        Task<bool> CreateAccountAsync(string email, string name);
    }
}
