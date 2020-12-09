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

            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0)
            {
                logger.LogError("File was empty");
            }
            else if (lines.Length == 1)
            {
                logger.LogWarning("Not enough destinations to compare...");
            }

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            logger.LogInfo("Begin parsing");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.WriteLine();
            var locations = lines.Select(parser.Parse).ToArray();

            ITrackable firstTB = null;
            ITrackable lastTB = null;
            double distBetween = 0;

            var geoA = new GeoCoordinate();
            var geoB = new GeoCoordinate();

            Console.WriteLine("Calculating Distances");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.Write(".");
            Thread.Sleep(1000);
            Console.WriteLine();

            foreach (var locA in locations)
            {
                geoA.Latitude = locA.Location.Latitude;
                geoA.Longitude = locA.Location.Longitude;

                foreach (var locB in locations)
                {
                    geoB.Latitude = locB.Location.Latitude;
                    geoB.Longitude = locB.Location.Longitude;

                    var newDist = geoA.GetDistanceTo(geoB);

                    if (newDist > distBetween)
                    {
                        distBetween = newDist;
                        firstTB = locA;
                        lastTB = locB;
                    }
                }
            }

            //for (int i = 0; i < locations.Length - 1; i++)
            //{
            //    var locA = locations[i];

            //    geoA.Latitude = locA.Location.Latitude;
            //    geoA.Longitude = locA.Location.Longitude;

            //    for (int j = i + 1; j < locations.Length; j++)
            //    {
            //        var locB = locations[j];
            //        geoB.Latitude = locB.Location.Latitude;
            //        geoB.Longitude = locB.Location.Longitude;

            //        var newDist = geoA.GetDistanceTo(geoB);

            //        if (newDist > distBetween)
            //        {
            //            distBetween = newDist;
            //            firstTB = locA;
            //            lastTB = locB;
            //        }

            //    }

            //}

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


            Console.WriteLine($"{firstTB.Name} and {lastTB.Name} are the two farthest apart, with a difference in distance of {ans} miles");

            Console.WriteLine("Press Enter to close...");
            Console.ReadLine();
            
        }
    }
}
