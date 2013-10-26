using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenueSearch
{
    /// <summary>
    /// Represents a client that can consume the Locu Venue Search API.
    /// </summary>
    public class VenueSearchClient
    {
        /// <summary>
        /// Send a VenueSearchRequest as an asynchronous operation.
        /// </summary>
        /// <param name="request">The VenueSearchRequest to send asynchronously.</param>
        /// <returns></returns>
        public async Task<VenueSearchResponse> SendAsync(VenueSearchRequest request)
        {
            if (string.IsNullOrEmpty(request.ApiKey))
                throw new ArgumentNullException("API Key not provided.");

            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, request.Uri);

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<VenueSearchResponse>(httpContent);

            response.Json = httpContent;

            return response;
        }
    }
}
