using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Domain;
using Kwetter.Services.KweetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.Persistence.Contexts
{
    public class KweetContext : DbContext, IKweetContext
    {
        public DbSet<Kweet> Kweets { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
        public KweetContext(DbContextOptions<KweetContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}
