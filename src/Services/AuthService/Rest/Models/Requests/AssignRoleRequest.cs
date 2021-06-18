using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Rest.Models.Requests
{
    public class AssignRoleRequest
    {
        [Required]
        public Guid targetId { get; set; }
        [Required]
        public string roleToAssign { get; set; }
    }
}
