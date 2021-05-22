using Kwetter.Services.AuthService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> AuthorizeAsync(string code);
    }
}
