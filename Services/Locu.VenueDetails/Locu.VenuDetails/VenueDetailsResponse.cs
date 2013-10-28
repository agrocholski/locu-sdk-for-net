using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenuDetails
{
    /// <summary>
    /// Represents a response from the Locu Venue Details API.
    /// </summary>
    public class VenueDetailsResponse
    {
        /// <summary>
        /// Metadata about the search results
        /// </summary>
        /// <remarks>
        /// Maps to the meta property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "meta")]
        public VenueDetailsResponseMetadata Metadata { get; set; }
    }

    /// <summary>
    /// Represents metadata associated with a reponse from the Locu
    /// Venue Details API.
    /// </summary>
    public class VenueDetailsResponseMetadata
    {
        /// <summary>
        /// Number of seconds the data can be cached
        /// </summary>
        /// <remarks>
        /// Maps to the cache-expiry property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "cache-expiry")]
        public int CacheExpiry { get; set; }
    }

    /// <summary>
    /// Represents the hours a venue is open.
    /// </summary>
    public class VenueDetailsOpenHours
    {
        /// <summary>
        /// Hours the venue is open on Mondays
        /// </summary>
        public List<string> Monday { get; set; }

        /// <summary>
        /// Hours the venue is open on Tuesdays 
        /// </summary>
        public List<string> Tuesday { get; set; }

        /// <summary>
        /// Hours the venue is open on Wednesdays
        /// </summary>
        public List<string> Wednesday { get; set; }

        /// <summary>
        /// Hours the venue is open on Thursdays
        /// </summary>
        public List<string> Thursday { get; set; }

        /// <summary>
        /// Hours the venue is open on Fridays
        /// </summary>
        public List<string> Friday { get; set; }

        /// <summary>
        /// Hours the venue is open on Saturdays
        /// </summary>
        public List<string> Saturday { get; set; }

        /// <summary>
        /// Hours the venue is open on Sundays
        /// </summary>
        public List<string> Sunday { get; set; }
    }
}
