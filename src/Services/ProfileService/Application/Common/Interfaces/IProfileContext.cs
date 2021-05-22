using Kwetter.Services.ProfileService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface IProfileContext
    {
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync();
    }
}
