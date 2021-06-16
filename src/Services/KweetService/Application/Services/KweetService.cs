using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Kwetter.Services.KweetService.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Domain;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class KweetService : IKweetService
    {
        private readonly IKweetContext _kweetContext;
        public KweetService(IKweetContext context)
        {
            _kweetContext = context;
        }
        public async Task<KweetDTO> CreateKweetAsync(Guid profileID, string kweetText)
        {
            Kweet kweetToPost = new Kweet()
            {
                KweetId = Guid.NewGuid(),
                KweetMessage = kweetText,
                KweetPostDate = DateTime.Now,
                SenderProfileId = profileID
            };
            await _kweetContext.Kweets.AddAsync(kweetToPost);
            bool success = await _kweetContext.SaveChangesAsync() > 0;

            if(!success)
            {
                throw new InvalidOperationException("Something went wrong trying to create the kweet");
            }
            return kweetToPost;
        }

        public Task DeleteKweet(Guid profileId, Guid kweetId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Kweet>> GetKweetsByProfilePaginated(int page, int pageSize, Guid profileId)
        {
            IEnumerable<Kweet> response = await _kweetContext.Kweets
                .OrderBy(x => x.KweetPostDate)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            //var profile = await _context.Profiles.FindAsync(profileId);

            if (response == null) return response;

            return response;
        }
    }
}
