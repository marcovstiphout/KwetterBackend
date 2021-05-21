using System;
using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.KweetService.Rest.Models.Requests
{
    public class CreateKweetRequest
    {
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string ProfileId { get; set; }
        [Required]
        [StringLength(100)]
        public string Message { get; set; }
    }
}
