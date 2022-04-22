using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Howest.Prog.Xamarin.UriSourceExtensions
{
    internal class HttpHelpers
    {
        public static async Task<byte[]> GetImageBytes(Uri uri, bool ignoreCertificateErrors, string token = null)
        {
            var httpClientHandler = new HttpClientHandler();

            if (Debugger.IsAttached && ignoreCertificateErrors)
            {
                //allow handshaking with untrusted certificates when running a DEBUG assembly
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    errors = System.Net.Security.SslPolicyErrors.None;
                    return true;
                };
            }

            using (HttpClient httpClient = new HttpClient(httpClientHandler))
            {
                if(token != null)
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync(uri);
                return await response.Content.ReadAsByteArrayAsync();
            }
        }
    }
}
