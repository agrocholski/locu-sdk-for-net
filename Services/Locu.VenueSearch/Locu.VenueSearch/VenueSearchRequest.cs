using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenueSearch
{
    /// <summary>
    /// Represents a request the Locu Venue Search API.
    /// </summary>
    public class VenueSearchRequest
    {
        /// <summary>
        /// Initialize a new instance of the VenueSearchRequest class with an API key.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        public VenueSearchRequest(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("apiKey", "An API key must be provided.");

            this.ApiKey = apiKey;
            this.Categories = new List<VenueCategory>();
            this.Cuisines = new List<string>();
            this.HasMenu = null;
            this.Radius = null;
            this.Location = new VenueSearchLocation();
        }

        /// <summary>
        /// Your Locu developer API key, that can be found on your profile page.
        /// </summary>
        /// <remarks>
        /// Maps to api_key parameter.
        /// </remarks>
        public string ApiKey { get; set; }

        /// <summary>
        /// Term to search for in venue names.
        /// </summary>
        /// <remarks>
        /// Maps to name parameter.
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Boolean indicating whether to search only for venues that have menus.
        /// </summary>
        /// <remarks>
        /// Maps to has_menue parameter.
        /// </remarks>
        public bool? HasMenu { get; set; }

        /// <summary>
        /// Venue categories to search within.
        /// </summary>
        /// <remarks>
        /// Maps to category parameter.
        /// </remarks>
        public List<VenueCategory> Categories { get; set; }

        /// <summary>
        /// Venue cuisine types to search within.
        /// </summary>
        /// <remarks>
        /// Maps to cuisine parameter.
        /// </remarks>
        public List<string> Cuisines { get; set; }

        /// <summary>
        /// Venue's website URL.
        /// </summary>
        /// <remarks>
        /// Maps to website_url parameter.
        /// </remarks>
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// Venue's stree address.
        /// </summary>
        /// <remarks>
        /// Maps to street_address parameter.
        /// </remarks>
        public string StreeAddress { get; set; }

        /// <summary>
        /// Locality (e.g. city, town name).
        /// </summary>
        /// <remarks>
        /// Maps to locality parameter.
        /// </remarks>
        public string Locality { get; set; }

        /// <summary>
        /// Region/state
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Venue's postal code.
        /// </summary>
        /// <remarks>
        /// Maps to postal_code parameter.
        /// </remarks>
        public string PostalCode { get; set; }

        /// <summary>
        /// Venue's country.
        /// </summary>
        /// <remarks>
        /// Maps to country parameter.
        /// </remarks>
        public string Country { get; set; }

        //todo: OpenAt
        //todo: Location

        /// <summary>
        /// Radius (in meters) to search within. This parameter
        /// can only be used together with location, it will be
        /// ignored otherwise. It is 50000 meters  by default.
        /// </summary>
        /// <remarks>
        /// Maps to the radius parameter.
        /// </remarks>
        public float? Radius { get; set; }

        /// <summary>
        /// Latitude and longitude of the location to search around.
        /// By default the search is within a 50000 meter radius.
        /// </summary>
        /// <remarks>
        /// Maps to the location parameter.
        /// </remarks>
        public VenueSearchLocation Location { get; set; }

        /// <summary>
        /// URI of the venue search request.
        /// </summary>
        public string Uri
        {
            get { return GetUri(); }
        }

        private string GetUri()
        {
            var uri = new StringBuilder(string.Format("https://api.locu.com/v1_0/venue/search/?api_key={0}", this.ApiKey));

            if (!string.IsNullOrEmpty(this.Locality))
                uri.Append(string.Format("&locality={0}", this.Locality));

            if (!string.IsNullOrEmpty(this.Region))
                uri.Append(string.Format("&region={0}", this.Region));

            if (this.HasMenu.HasValue)
                uri.Append(string.Format("&has_menu={0}", this.HasMenu.Value.ToString().ToLower()));

            if (!string.IsNullOrEmpty(this.PostalCode))
                uri.Append(string.Format("&postal_code={0}", this.PostalCode));

            if (!string.IsNullOrEmpty(this.Country))
                uri.Append(string.Format("&country={0}", this.Country));

            if (!string.IsNullOrEmpty(this.StreeAddress))
                uri.Append(string.Format("&street_address={0}", this.StreeAddress));

            if (!string.IsNullOrEmpty(this.Name))
                uri.Append(string.Format("&name={0}", this.Name));

            if (!string.IsNullOrEmpty(this.WebsiteUrl))
                uri.Append(string.Format("&website_url={0}", this.WebsiteUrl));

            var categories = GetCategorieList(this.Categories);

            if (!string.IsNullOrEmpty(categories))
                uri.Append(string.Format("&category={0}", categories));

            var cuisines = GetList(this.Cuisines);

            if (!string.IsNullOrEmpty(cuisines))
                uri.Append(string.Format("&cuisine={0}", cuisines));

            if (this.Radius.HasValue)
                uri.Append(string.Format("&radius={0}", this.Radius.Value.ToString()));
            
            if(this.Location != null && this.Location.Latitude.HasValue && this.Location.Longitude.HasValue)
                uri.Append(string.Format("&location={0},{1}", this.Location.Latitude.Value, this.Location.Longitude.Value));

            return uri.ToString();
        }

        private string GetCategorieList(List<VenueCategory> categories)
        {
            if (categories != null && categories.Count > 0)
            {
                var result = new StringBuilder();

                for (int i = 0; i < categories.Count; i++)
                {
                    var categoryName = GetCategoryName(categories[i]);

                    if (i == (categories.Count - 1))
                        result.Append(categoryName);
                    else
                        result.Append(string.Format("{0},", categoryName));
                }

                return result.ToString();
            }
            else
                return string.Empty;
        }

        private string GetCategoryName(VenueCategory category)
        {
            var result = "unknown";

            switch (category)
            {
                case VenueCategory.BeautySalon:
                    result = "beauty salon";
                    break;

                case VenueCategory.Gym:
                    result = "gym";
                    break;

                case VenueCategory.HairCare:
                    result = "hair care";
                    break;

                case VenueCategory.Laundry:
                    result = "laundry";
                    break;

                case VenueCategory.Other:
                    result = "other";
                    break;

                case VenueCategory.Restaurant:
                    result = "restaurant";
                    break;

                case VenueCategory.Spa:
                    result = "spa";
                    break;
            }

            return result;
        }

        private string GetList(List<string> values)
        {
            if (values != null && values.Count > 0)
            {
                var result = new StringBuilder();

                for (int i = 0; i < values.Count; i++)
                {
                    if (i == (values.Count - 1))
                        result.Append(values[i]);
                    else
                        result.Append(string.Format("{0},", values[i]));
                }

                return result.ToString();
            }
            else
                return string.Empty;
        }
    }

    /// <summary>
    /// Enumeration of possible venue categories
    /// </summary>
    public enum VenueCategory
    {
        /// <summary>
        /// Maps to "restaurant" category.
        /// </summary>
        Restaurant,
        /// <summary>
        /// Maps to "spa" category.
        /// </summary>
        Spa,
        /// <summary>
        /// Maps to "beauty salon" category.
        /// </summary>
        BeautySalon,
        /// <summary>
        /// Maps to "gym" category.
        /// </summary>
        Gym,
        /// <summary>
        /// Maps to "laundry" category.
        /// </summary>
        Laundry,
        /// <summary>
        /// Maps to "hair care" category.
        /// </summary>
        HairCare,
        /// <summary>
        /// Maps to "other" category.
        /// </summary>
        Other
    }

    /// <summary>
    /// Represents a location used in the Locu Venue Search API.
    /// </summary>
    public class VenueSearchLocation
    {
        /// <summary>
        /// Latitude to search.
        /// </summary>
        public float? Latitude { get; set; }

        /// <summary>
        /// Longitude to search.
        /// </summary>
        public float? Longitude { get; set; }
    }
}
