using System.Text.Json;
using dotnetnbpgold.nbp.client.Exceptions;

namespace dotnetnbpgold.nbp.client.Helpers
{
    internal static class HttpHelpers
    {
        internal static async Task<T?> HttpGetAsync<T>(string url)
        {
            var httpClient = new HttpClient();
            var response  = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode) {
                throw new DotNetNBPGoldClientException($"Connection error, url: {url}, response code: {response.StatusCode}.");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString);
        }
    }
}