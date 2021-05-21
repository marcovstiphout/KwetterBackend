using Kwetter.Services.KweetService.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class KweetDTO
    {
        public Guid KweetId { get; set; }

        public Guid SenderProfileId { get; set; }

        public string KweetMessage { get; set; }

        public DateTime KweetPostDate { get; set; }

        public static implicit operator KweetDTO(Kweet v)
        {
            KweetDTO kweet = new KweetDTO()
            {
                KweetId = v.KweetId,
                SenderProfileId = v.SenderProfileId,
                KweetMessage = v.KweetMessage,
                KweetPostDate = v.KweetPostDate
            };
            return kweet;
        }
    }
}
