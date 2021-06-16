using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.Rest.Models.Requests
{
    public class GetKweetsRequest
    {
        [Required]
        [Range(0, 100)]
        public int PageSize { get; set; }

        [Required]
        public int PageNumber { get; set; }

        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string ProfileId { get; set; }
    }
}
