using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenueDetails
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

        /// <summary>
        /// List of Locu ID's that could not be found
        /// </summary>
        [JsonProperty(PropertyName = "not_found")]
        public List<string> NotFound { get; set; }

        /// <summary>
        /// Venue details returned
        /// </summary>
        [JsonProperty(PropertyName = "objects")]
        public List<VenueDetailResponseObject> Venues { get; set; }

        /// <summary>
        /// Raw json response from the search
        /// </summary>
        [JsonIgnore]
        public string Json { get; set; }

        /// <summary>
        /// Creates an instance of the VenueDetailsResponse class from a string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static VenueDetailsResponse CreateFromString(string source)
        {
            return JsonConvert.DeserializeObject<VenueDetailsResponse>(source);
        }
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
    /// Represents venue details returned from the Locu Venu Details API.
    /// </summary>
    public class VenueDetailResponseObject
    {
        /// <summary>
        /// The venue's Locu id.
        /// </summary>
        /// <remarks>
        /// Maps to the id property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of the venue.
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
        /// The phnone number of the venue.
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
        /// The street address of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the street_address property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "street_address")]
        public string StreeAddress { get; set; }

        /// <summary>
        /// The ocality of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the locality property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "locality")]
        public string Locality { get; set; }

        /// <summary>
        /// The region of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the region property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "region")]
        public string Region { get; set; }

        /// <summary>
        /// The postal code of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the postal_code property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// The country of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the country property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "country")]
        public string Country { get; set; }

        /// <summary>
        /// The latitude of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the lat property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "lat")]
        public float Latitude { get; set; }

        /// <summary>
        /// The longitude of the venue.
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
        /// List of the venue's cuisines.
        /// </summary>
        /// <remarks>
        /// Maps to the cuisines property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "cuisines")]
        public List<string> Cuisines { get; set; }

        /// <summary>
        /// URL to venue's facebook page.
        /// </summary>
        /// <remarks>
        /// Maps to the facebook_url property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "facebook_url")]
        public string FacebookUrl { get; set; }

        /// <summary>
        /// Venue's twitter id.
        /// </summary>
        /// <remarks>
        /// Maps to the twitter_id property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "twitter_id")]
        public string TwitterId { get; set; }

        /// <summary>
        /// Hours the venue is open.
        /// </summary>
        /// <remarks>
        /// Maps to the open_hours property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "open_hours")]
        public VenueDetailsOpenHours OpenHours { get; set; }

        /// <summary>
        /// Menus of the venue.
        /// </summary>
        /// <remarks>
        /// Maps to the menus property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "menus")]
        public List<Menu> Menus { get; set; }
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
