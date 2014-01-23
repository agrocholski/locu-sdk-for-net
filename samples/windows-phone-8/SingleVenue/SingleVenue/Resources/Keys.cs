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
         * 1. After you have finished your app, begin the app submission process.
         * 2. On the SUBMIT APP page, click MAP SERVICES
         * 3. On the page click, GET TOKEN
         *    The new ApplicationId and AuthenticationToken are displayed on the same page
         * 4. Uncomment the code below.
         * 5. Copy the ApplicationId and AuthenticationToken values and paste them into the corresponding properties below.
         * 6. Rebuild your app with the new code and upload to the Store.
         * */

        public static string LocuApiKey
        {
            get { return ""; }
        }

        public static string LocuVenuId
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
