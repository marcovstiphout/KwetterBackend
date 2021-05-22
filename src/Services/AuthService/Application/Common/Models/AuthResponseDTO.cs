using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kwetter.Services.AuthService.Application.Common.Models
{
    public class AuthResponseDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("user_id")]
        public Guid UserId { get; set; }
    }
}
