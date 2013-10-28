using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Locu.VenueDetails
{
    /// <summary>
    /// Represents a menu returned from the Locu Venue Details API.
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// Name of the menu.
        /// </summary>
        /// <remarks>
        /// Maps to the menu_name property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "menu_name")]
        public string Name { get; set; }

        /// <summary>
        /// Sections of the menu.
        /// </summary>
        /// <remarks>
        /// Maps the sections property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "sections")]
        public List<MenuSection> Sections { get; set; }
    }

    /// <summary>
    /// Represents a menu section returned from the Locu Venue Details API.
    /// </summary>
    public class MenuSection
    {
        /// <summary>
        /// The name of the section.
        /// </summary>
        /// <remarks>
        /// Maps to the section_name property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "section_name")]
        public string SectionName { get; set; }

        /// <summary>
        /// Subsections within the menu section.
        /// </summary>
        /// <remarks>
        /// Maps to the subsections property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "subsections")]
        public List<MenuSubsection> Subsections { get; set; }
    }

    /// <summary>
    /// Represents a menu subsection returned from the Locu Venue Details API.
    /// </summary>
    public class MenuSubsection
    {
        /// <summary>
        /// Name of the subsection.
        /// </summary>
        /// <remarks>
        /// Maps to the subsection_name property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "subsection_name")]
        public string SubsectionName { get; set; }

        /// <summary>
        /// Contents of the subsection.
        /// </summary>
        /// <remarks>
        /// Maps to the contents propety of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "contents")]
        public List<MenuContent> Contents { get; set; }
    }

    /// <summary>
    /// Represents menu content returned from the Locu Venue Details API.
    /// </summary>
    public class MenuContent
    {
        /// <summary>
        /// The type of content.
        /// </summary>
        /// <remarks>
        /// Maps to the type property of the response object.
        /// Value will either be MENU_ITEM or SECTION_TEXT.
        /// </remarks>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Text of the content
        /// </summary>
        /// <remarks>
        /// Maps to the text property of the response object.
        /// Applies to both MENU_ITEM and SECTION_TEXT content types.
        /// </remarks>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Name of the content.
        /// </summary>
        /// <remarks>
        /// Maps to the name property of the response object.
        /// Applies only to the MENU_ITEM content type.
        /// </remarks>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the content.
        /// </summary>
        /// <remarks>
        /// Maps to the description property of the response object.
        /// Applies only to the MENU_ITEM content type.
        /// </remarks>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Price of the content.
        /// </summary>
        /// <remarks>
        /// Maps to the price property of the response object.
        /// Applies only to the MENU_ITEM content type.
        /// </remarks>
        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }

        /// <summary>
        /// Option groups associated with the content.
        /// </summary>
        /// <remarks>
        /// Maps to the option_groups property of the reponse object.
        /// Applies only to the MENU_ITEM content type.
        /// </remarks>
        [JsonProperty(PropertyName = "option_groups")]
        public List<MenuOptionGroup> OptionGroups { get; set; }
    }

    /// <summary>
    /// Represents a menu option group returned from the Locu Venue Details API.
    /// </summary>
    public class MenuOptionGroup
    {
        /// <summary>
        /// The type of menu option group.
        /// </summary>
        /// <remarks>
        /// Maps to the type property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The text of the menu option group.
        /// </summary>
        /// <remarks>
        /// Maps to the text property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Options associated with the option group.
        /// </summary>
        /// <remarks>
        /// Maps to the options property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "options")]
        public List<MenuOption> Options { get; set; }
    }

    /// <summary>
    /// Represents a menu option returned from the Locu Venue Details API.
    /// </summary>
    public class MenuOption
    {
        /// <summary>
        /// The name of the menu option.
        /// </summary>
        /// <remarks>
        /// Maps to the name property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The price of the menu option.
        /// </summary>
        /// <remarks>
        /// Maps to the price property of the response object.
        /// </remarks>
        [JsonProperty(PropertyName = "price")]
        public string Price { get; set; }
    }
}
