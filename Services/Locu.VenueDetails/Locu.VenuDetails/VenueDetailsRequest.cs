using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenuDetails
{
    public class VenueDetailsRequest
    {
        /// <summary>
        /// Initializes a new instances of the VenueDetailsRequest class
        /// with an API key and Venue ID.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="venueId">The ID of the venue.</param>
        public VenueDetailsRequest(string apiKey, string venueId)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey", "An API key must be provided.");

            if (string.IsNullOrEmpty(venueId))
                throw new ArgumentNullException("venueId", "A Venue ID must be provided.");

            this.ApiKey = apiKey;
            this.VenueId = venueId;
        }

        /// <summary>
        /// Your Locu developer API key, that can be found on your profile page.
        /// </summary>
        /// <remarks>
        /// Maps to the api_key parameter.
        /// </remarks>
        public string ApiKey { get; set; }

        /// <summary>
        /// The Locu ID of then venu
        /// </summary>
        public string VenueId { get; set; }

        /// <summary>
        /// URI of the venue detail request
        /// </summary>
        public string Uri
        {
            get { return GetUri(); }
        }

        private string GetUri()
        {
            if (string.IsNullOrEmpty(this.ApiKey))
                throw new ArgumentNullException("apiKey", "An API key must be provided.");

            if (string.IsNullOrEmpty(this.VenueId))
                throw new ArgumentNullException("venueId", "A Venue ID must be provided.");

            return string.Format("http://api.locu.com/v1_0/venue/{0}/?api_key={1}", this.VenueId, this.ApiKey);
        }
    }
}
