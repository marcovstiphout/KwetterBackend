using Kwetter.Services.AuthService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces.Services
{
    public interface IAuthoService
    {
        Task<UserDTO> SetUserClaims(string uid);
        Task<bool> AssignElevatedPermissions(Guid userToElevate, string roleToAssign);
    }
}
