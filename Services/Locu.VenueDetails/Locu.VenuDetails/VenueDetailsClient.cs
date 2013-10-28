using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenuDetails
{
    /// <summary>
    /// Represents a client that can consume the Locu Venue Details API.
    /// </summary>
    public class VenueDetailsClient
    {
        /// <summary>
        /// Sends a VenueDetailsRequest as an asynchronous operation.
        /// </summary>
        /// <param name="request">The VenueDetailsRequest to send asynchronously.</param>
        /// <returns></returns>
        public async Task<VenueDetailsResponse> SendAsync(VenueDetailsRequest request)
        {
            if (string.IsNullOrEmpty(request.ApiKey))
                throw new ArgumentNullException("API Key not provided.");

            if (string.IsNullOrEmpty(request.VenueId))
                throw new ArgumentNullException("Venue Id not provided.");

            var handler = new HttpClientHandler();
            var client = new HttpClient(handler);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, request.Uri);

            var httpResponseMessage = await client.SendAsync(httpRequestMessage);

            var httpContent = await httpResponseMessage.Content.ReadAsStringAsync();

            var response = JsonConvert.DeserializeObject<VenueDetailsResponse>(httpContent);

            response.Json = httpContent;

            return response;
        }
    }
}
