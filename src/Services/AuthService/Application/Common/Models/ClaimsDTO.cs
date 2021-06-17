using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Application.Common.Models
{
    public class ClaimsDTO
    {
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long ExpirationTimeSeconds { get; set; }
        public long IssuedAtTimeSeconds { get; set; }
        public IReadOnlyDictionary<string, object> Claims { get; set; }
    }
}
