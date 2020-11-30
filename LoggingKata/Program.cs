using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Threading;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // TODO:  Find the two Taco Bells that are the furthest from one another.
            // HINT:  You'll need two nested forloops ---------------------------

            logger.LogInfo("Log initialized");

            // use File.ReadAllLines(path) to grab all the lines from your csv file
            // Log and error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");//need to change this later as well as in the for loops

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Grab an IEnumerable of locations using the Select command: var locations = lines.Select(parser.Parse);
            logger.LogInfo("Begin parsing");
            var locations = lines.Select(parser.Parse).ToArray();


            // DON'T FORGET TO LOG YOUR STEPS

            // Now that your Parse method is completed, START BELOW ----------

            // TODO: Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the farthest from each other.
            ITrackable firstTB = null;
            ITrackable lastTB = null;
            // Create a `double` variable to store the distance

            double distBetween = 0;
            var geoA = new GeoCoordinate();
            var geoB = new GeoCoordinate();
            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`

            //HINT NESTED LOOPS SECTION---------------------

            Console.WriteLine("Calculating Distances");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.WriteLine();
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            for (int i = 0; i < locations.Length - 1; i++)
            {
                var locA = locations[i];
                // Create a new corA Coordinate with your locA's lat and long
                geoA.Latitude = locA.Location.Latitude;
                geoA.Longitude = locA.Location.Longitude;
                // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
                for (int j = i + 1; j < locations.Length; j++)
                {
                    // Create a new Coordinate with your locB's lat and long
                    var locB = locations[j];
                    geoB.Latitude = locB.Location.Latitude;
                    geoB.Longitude = locB.Location.Longitude;

                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    var newDist = geoA.GetDistanceTo(geoB);

                    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                    if (newDist > distBetween)
                    {
                        distBetween = newDist;
                        firstTB = locA;
                        lastTB = locB;
                    }

                }

            }
            // Once you've looped through everything, you've found the two Taco Bells farthest away from each other.


            //conver from meters to miles
            var ans = distBetween * 0.00062137;
            ans = Math.Round(ans, 2);
            logger.LogInfo("Converting to miles");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.WriteLine();

            //write everything to the console
            Console.WriteLine($"{firstTB.Name} and {lastTB.Name} are the two farthest apart, with a difference in distance of {ans} miles");

            Console.WriteLine("Press Enter to close...");
            Console.ReadLine();
            
        }
    }
}
