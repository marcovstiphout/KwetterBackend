using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Rest.Models.Requests
{
    public class AuthorizationRequest
    {
        [Required]
        public string Code { get; set; }
    }
}
