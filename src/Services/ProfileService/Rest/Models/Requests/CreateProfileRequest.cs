using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class CreateProfileRequest
    {
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string ProfileId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProfileName { get; set; }
    }
}
