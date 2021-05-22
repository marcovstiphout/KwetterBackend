using Kwetter.Services.AuthService.Application.Common.Models;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthHttpRequest
    {
        Task<AuthResponseDto> SendAuthRequest(string code);
    }
}
