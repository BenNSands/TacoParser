

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

            var cells = line.Split(',');
            
            if (cells.Length < 3)
            {
                logger.LogError("Less than 3 Items in array");
                return null;
            }

            double lat;
            double lon;


            try
            {
                lat = double.Parse(cells[0]);
                lon = double.Parse(cells[1]);
            }
            catch (System.Exception)
            {

                return null;
            }

            string tacoBellName = cells[2];

            var point = new Point() { Latitude = lat, Longitude = lon };
            var tacoBell = new TacoBell() { Name = tacoBellName, Location = point};

            return tacoBell;
        }
    }
}