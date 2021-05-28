using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthHttpRequest
    {
        Task<bool> SendAuthRequest(string code);
    }
}
