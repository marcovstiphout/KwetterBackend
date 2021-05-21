using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.KweetService.Domain
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public Guid KweetId { get; set; }
    }
}
