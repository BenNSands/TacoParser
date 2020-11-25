namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            //DONE Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');
            

            //DONE If your array.Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                //DONE Log that and return null
                logger.LogError("Less than 3 Items in array");
                //DONE Do not fail if one record parsing fails, return null
                return null; // TODO Implement
            }

            // DONE grab the latitude from your array at index 0
            var lat = double.Parse(cells[0]);
            // DONE grab the longitude from your array at index 1
            var lon = double.Parse(cells[1]);
            // DONE grab the name from your array at index 2
            var tacoBellName = cells[2];
            // DONE Your going to need to parse your string as a `double`
            // DONE which is similar to parsing a string as an `int`

            // DONE You'll need to create a TacoBell class
            // DONE that conforms to ITrackable

            // Then, you'll need an instance of the TacoBell class
            // With the name and point set correctly
            var tacoBell = new TacoBell();
            tacoBell.Name = tacoBellName;
            var point = new double[] {lat, lon};
            tacoBell.Location.Latitude = lat;


            // Then, return the instance of your TacoBell class
            // Since it conforms to ITrackable

            return null;
        }
    }
}