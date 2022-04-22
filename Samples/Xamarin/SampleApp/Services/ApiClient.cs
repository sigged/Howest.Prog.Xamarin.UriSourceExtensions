using SampleApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace SampleApp.Services
{
    public class ApiClient
    {
        private const string baseUrl = "https://172.30.138.31:5001";
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ApiClient()
        {
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }


        public async Task<AuthResult> LoginAsync()
        {

            using (var httpClient = new HttpClient(CreateHandler()))
            {
                var response = await httpClient.PostAsync($"{baseUrl}/account/login", null);
                var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<AuthResult>(stream, _jsonSerializerOptions);
            }
        }

        public async Task<IEnumerable<ImageDto>> GetPublicImagesAsync()
        {

            using (var httpClient = new HttpClient(CreateHandler()))
            {
                var stream = await httpClient.GetStreamAsync($"{baseUrl}/images/public");
                var images = await JsonSerializer.DeserializeAsync<IEnumerable<ImageDto>>(stream, _jsonSerializerOptions);
                images.ToList().ForEach(image => image.Url = $"{baseUrl}{image.Url}");
                return images;
            }
        }

        public async Task<IEnumerable<ImageDto>> GetProtectedImagesAsync(string token)
        {
            using (var httpClient = new HttpClient(CreateHandler()))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var stream = await httpClient.GetStreamAsync($"{baseUrl}/images/protected");
                var images = await JsonSerializer.DeserializeAsync<IEnumerable<ImageDto>>(stream, _jsonSerializerOptions);
                images.ToList().ForEach(image => image.Url = $"{baseUrl}{image.Url}");
                return images;
            }
        }
        private HttpClientHandler CreateHandler()
        {
            var clientHandler = new HttpClientHandler();
#if DEBUG
            //allow handshaking with untrusted certificates when running a DEBUG assembly
            clientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => {
                errors = System.Net.Security.SslPolicyErrors.None;
                return true;
            };
#endif
            return clientHandler;
        }
    }
}
