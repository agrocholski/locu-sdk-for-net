using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenueDetails
{
    public class VenueDetailsRequest
    {
        /// <summary>
        /// Initializes a new instances of the VenueDetailsRequest class
        /// with an API key and Venue ID.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="venueIds">The IDs of the venues.</param>
        public VenueDetailsRequest(string apiKey, List<string> venueIds)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey", "An API key must be provided.");

            if (venueIds == null || venueIds.Count == 0)
                throw new ArgumentNullException("venueIds", "At least one Venue ID must be provided.");

            this.ApiKey = apiKey;
            this.VenueIds = venueIds;
        }

        /// <summary>
        /// Your Locu developer API key, that can be found on your profile page.
        /// </summary>
        /// <remarks>
        /// Maps to the api_key parameter.
        /// </remarks>
        public string ApiKey { get; set; }

        /// <summary>
        /// The Locu IDs of then venue
        /// </summary>
        public List<string> VenueIds { get; set; }

        /// <summary>
        /// URIs of the venue detail request
        /// </summary>
        public List<string> Uris
        {
            get { return GetUris(); }
        }

        private List<string> GetUris()
        {
            if (string.IsNullOrEmpty(this.ApiKey))
                throw new ArgumentNullException("apiKey", "An API key must be provided.");

            if (this.VenueIds == null || this.VenueIds.Count == 0)
                throw new ArgumentNullException("venueIds", "At least one Venue ID must be provided.");

            var result = new List<string>();

            foreach (var venueId in this.VenueIds)
                result.Add(string.Format("http://api.locu.com/v1_0/venue/{0}/?api_key={1}", venueId, this.ApiKey));

            return result;
        }
    }
}
