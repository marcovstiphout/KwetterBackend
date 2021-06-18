using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain;
using Kwetter.Services.KweetService.Domain.MongoEntities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class MongoKweetService : IKweetService
    {
        private readonly IMongoCollection<MongoKweet> _kweets;
        private readonly IMongoDatabase database;
        private readonly MongoClient client;
        public MongoKweetService(IMongoDatabaseSettings settings)
        {
            client = new MongoClient(settings.ConnectionString);
            database = client.GetDatabase(settings.DatabaseName);

            _kweets = database.GetCollection<MongoKweet>(settings.CollectionName);
        }
        public async Task<KweetDTO> CreateKweetAsync(Guid profileID, string kweetText)
        {
            Kweet kweetToPost = new Kweet()
            {
                KweetMessage = kweetText,
                KweetPostDate = DateTime.Now,
                SenderProfileId = profileID
            };

            await _kweets.InsertOneAsync(kweetToPost);

            return kweetToPost;
        }

        public Task DeleteKweet(Guid profileId, Guid kweetId)
        {
            _kweets.DeleteOne(kweet => kweet.KweetId == kweetId.ToString());
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<Kweet>> GetKweetsByProfilePaginated(int page, int pageSize, Guid profileId)
        {
            IEnumerable<Kweet> response = (IEnumerable<Kweet>)_kweets.Find(book => true).Limit(pageSize).Skip(page * pageSize);

            return response;
        }
    }
}
