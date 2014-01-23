using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleVenue.Resources
{
    public class Keys
    {
        //TODO: Get an ApplicationID and AuthenticationToken from the Dev Center
        /*
         * 1. Get a Locu API key at https://dev.locu.com/. Use your key for the value of the LocuApiKey property.
         * 2. Obtain the Locu Id of the venue you want to build the app for. Use the id for the value of teh LocuVenuId property.
         *      Note: You can either use the Locu developer web console (https://dev.locu.com/console/) or 
         *      the Locu venue details console that is part of the Locu SDK for .NET (https://github.com/agrocholski/locu-sdk-for-net).
         * 3. After you have finished your app, begin the app submission process to submit it to the Windows Phone store.
         * 4. On the SUBMIT APP page, click MAP SERVICES
         * 5. On the page click, GET TOKEN
         *    The new ApplicationId and AuthenticationToken are displayed on the same page
         * 7. Copy the ApplicationId and AuthenticationToken values and paste them into the corresponding properties below.
         * 8. Rebuild your app with the new code and upload to the Store.
         * */

        public static string LocuApiKey
        {
            get { return ""; }
        }

        public static string LocuVenueId
        {
            get { return ""; }
        }

        public static string MapsApplicationId
        {
            get { return ""; }
        }

        public static string MapsAuthenticationToken
        {
            get { return ""; }
        }
    }
}
