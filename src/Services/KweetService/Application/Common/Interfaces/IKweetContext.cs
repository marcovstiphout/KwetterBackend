using Kwetter.Services.KweetService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IKweetContext
    {
        DbSet<Kweet> Kweets { get; set; }
        Task<int> SaveChangesAsync();
    }
}
