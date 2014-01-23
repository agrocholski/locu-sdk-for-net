using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Locu.VenueDetails;
using System.IO;

namespace Locu.VenueDetails
{
    class Program
    {
        private static bool interaciveMode;

        static void Main(string[] args)
        {
            try
            {
                string apiKey = "";
                string venueId = "";
                string outputPath = "";
                List<string> venueIds = new List<string>();

                if (args.Length == 3)
                {
                    interaciveMode = false;

                    apiKey = args[0];
                    Console.WriteLine("API Key: {0}", apiKey);

                    if(args[1].Contains(","))
                       venueIds = args[1].Split(new char[] { ',' }).ToList();
                    else
                        venueIds.Add(args[1]);

                    venueId = args[1];
                    Console.WriteLine("Venue Id: {0}", venueId);

                    outputPath = args[2];
                    Console.Write("Output Path: {0}", outputPath);
                }
                else
                {
                    interaciveMode = true;

                    Console.WriteLine("API Key:");
                    apiKey = Console.ReadLine();

                    Console.WriteLine("Venue Id:");
                    venueId = Console.ReadLine();

                    Console.WriteLine("Output Path:");
                    outputPath = Console.ReadLine();
                }

                GetData(apiKey, venueIds, outputPath);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                if (interaciveMode)
                {
                    Console.WriteLine("Processing complete, press any key to exit.");
                    Console.ReadLine();
                }
                else
                {
                    Environment.Exit(0);
                }
            }

            Console.WriteLine("Processing");
            Console.ReadLine();
        }

        private static async void GetData(string apiKey, List<string> venueIds, string outputPath)
        {
            Console.WriteLine("Creating request...");
            var request = new VenueDetailsRequest(apiKey, venueIds);

            Console.WriteLine("Creating client...");
            var client = new VenueDetailsClient();

            Console.WriteLine("Sending request...");
            var venues = await client.SendAsync(request);
            Console.WriteLine("Response recevied...");

            Console.WriteLine("Creating file...");
            if (File.Exists(outputPath))
                File.Delete(outputPath);

            StringBuilder json = new StringBuilder();

            foreach(var venue in venues)
                json.Append(venue.Json);

            using(StreamWriter file = new StreamWriter(@outputPath))
            {
                file.Write(json.ToString());
                Console.WriteLine("File created...");
            }

            if (interaciveMode)
            {
                Console.WriteLine("Processing complete, press any key to exit.");
                Console.ReadLine();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
