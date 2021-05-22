using Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
