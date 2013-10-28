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

                if (args.Length == 3)
                {
                    interaciveMode = false;

                    apiKey = args[0];
                    Console.WriteLine("API Key: {0}", apiKey);

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

                GetData(apiKey, venueId, outputPath);
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

        private static async void GetData(string apiKey, string venueId, string outputPath)
        {
            Console.WriteLine("Creating request...");
            var request = new VenueDetailsRequest(apiKey, venueId);

            Console.WriteLine("Creating client...");
            var client = new VenueDetailsClient();

            Console.WriteLine("Sending request...");
            var response = await client.SendAsync(request);
            Console.WriteLine("Response recevied...");

            Console.WriteLine("Creating file...");
            if (File.Exists(outputPath))
                File.Delete(outputPath);

            using(StreamWriter file = new StreamWriter(@outputPath))
            {
                file.Write(response.Json);
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
