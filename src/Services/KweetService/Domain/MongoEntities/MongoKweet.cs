using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Domain.MongoEntities
{
    public class MongoKweet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string KweetId { get; set; }

        public Guid SenderProfileId { get; set; }

        public string KweetMessage { get; set; }

        public DateTime KweetPostDate { get; set; }

        public static implicit operator MongoKweet(Kweet v)
        {
            MongoKweet kweet = new MongoKweet()
            {
               // KweetId = v.KweetId.ToString(),
                SenderProfileId = v.SenderProfileId,
                KweetMessage = v.KweetMessage,
                KweetPostDate = v.KweetPostDate
            };
            return kweet;
        }
    }
}
