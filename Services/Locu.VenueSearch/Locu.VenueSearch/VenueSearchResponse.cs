using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenueSearch
{
    /// <summary>
    /// Represents a response from Locu Venue Search API.
    /// </summary>
    public class VenueSearchResponse
    {
        /// <summary>
        /// Metadata about the search results.
        /// </summary>
        /// <remarks>
        /// Maps to the meta property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "meta")]
        public VenuSearchResponseMetadata Metadata { get; set; }

        /// <summary>
        /// Venues returned from the search.
        /// </summary>
        /// <remarks>
        /// Maps to the objects property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "objects")]
        public List<VenueSearchResponseObject> Venues { get; set; }

        /// <summary>
        /// Raw json response from the search.
        /// </summary>
        [JsonIgnore]
        public string Json { get; set; }
    }

    /// <summary>
    /// Represents metadata associated with a response from the Locu Venu Search API.
    /// </summary>
    public class VenuSearchResponseMetadata
    {
        /// <summary>
        /// Number of seconds the data can be cached.
        /// </summary>
        /// <remarks>
        /// Maps to the cache-expiry property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "cache-expiry")]
        public int CacheExpiry { get; set; }

        /// <summary>
        /// Number of results per page - set to 25.
        /// </summary>
        /// <remarks>
        /// Maps to the limit property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "limit")]
        public int Limit { get; set; }
    }

    /// <summary>
    /// Represents a venue returned from the Locu Venue Search API.
    /// </summary>
    public class VenueSearchResponseObject
    {
        /// <summary>
        /// Venue Locu id
        /// </summary>
        /// <remarks>
        /// Maps to the id property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Name of the venue
        /// </summary>
        /// <remarks>
        /// Maps to the name property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// URL of the venue website.
        /// </summary>
        /// <remarks>
        /// Maps to the website_url property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "website_url")]
        public string WebSiteUrl { get; set; }

        /// <summary>
        /// Whether or not the venue has a menu.
        /// </summary>
        /// <remarks>
        /// Maps to the has_menu property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "has_menu")]
        public bool HasMenu { get; set; }

        /// <summary>
        /// Phone number of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the phone property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// URI for accessing the venue details.
        /// </summary>
        /// <remarks>
        /// Maps to the resource_uri property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "resource_uri")]
        public string ResourceUri { get; set; }

        /// <summary>
        /// Street address of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the street_address property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "street_address")]
        public string StreeAddress { get; set; }

        /// <summary>
        /// Locality of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the locality property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        /// <summary>
        /// Region of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the region property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// Postal code of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the postal_code property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country the venue is located in.
        /// </summary>
        /// <remarks>
        /// Maps to the country property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// Latitude of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the lat property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the long property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "long")]
        public float Longitude { get; set; }

        /// <summary>
        /// List of categories (e.g. spa, restaurant) that the venue belongs to.
        /// </summary>
        /// <remarks>
        /// Maps to the categories property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "categories")]
        public List<string> Categories { get; set; }

        /// <summary>
        /// List of cuisines the venue has.
        /// </summary>
        /// <remarks>
        /// Maps to the cuisines property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "cuisines")]
        public List<string> Cuisines { get; set; }
    }
}
