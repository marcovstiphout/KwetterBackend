using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces.Services
{
    public interface IKweetService
    {
        Task<KweetDTO> CreateKweetAsync(Guid profileID, string kweetText);
        Task<IEnumerable<Kweet>> GetKweetsByProfilePaginated(int page, int pageSize, Guid profileId);
        Task DeleteKweet(Guid profileId, Guid kweetId);
    }
}
