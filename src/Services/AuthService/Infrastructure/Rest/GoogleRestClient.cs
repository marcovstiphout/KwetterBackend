using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Kwetter.Services.AuthService.Infrastructure.Rest
{
    public class GoogleRestClient : IAuthHttpRequest
    {
        private readonly HttpClient _client;
        private readonly RequestConfig _config;

        public GoogleRestClient(HttpClient client, RequestConfig config)
        {
            _client = client;
            _config = config;
        }

        public async Task<AuthResponseDto> SendAuthRequest(string code)
        {
            AuthResponseDto response = new AuthResponseDto();

            string codeQuery =
                $"code={code}" +
                $"&client_id={_config.ClientId}" +
                $"&client_secret={_config.ClientSecret}" +
                $"&redirect_uri=postmessage" +
                $"&grant_type=authorization_code";

            HttpContent content = new StringContent(codeQuery, Encoding.UTF8,
                "application/x-www-form-urlencoded");

            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://oauth2.googleapis.com/token"),
                Content = content
            };
            HttpResponseMessage httpResponseMessage = await _client.SendAsync(message);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string json = await httpResponseMessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<AuthResponseDto>(json);
            }
            return response;
        }
    }
}
