using System.IO;

namespace ProcessorDublinBusData
{
    public static class FileWriter
    {
        #region Route Outputs
        public static void RouteInfo(StreamWriter fileWriter, string route, int total, int duplicate)
        {
            fileWriter.WriteLine(@"----------------------------------------------------------------");
            fileWriter.WriteLine(string.Format(@"Route: {0} Total: {1} Stops Duplicate: {2}", route, total, duplicate));
            fileWriter.WriteLine();
        }

        public static void RoutesCompleteReport(StreamWriter fileWriter, int totalRoutes)
        {
            fileWriter.WriteLine(string.Format(@"Total Routes: {0}", totalRoutes));
        }

        public static void RouteRecord(StreamWriter fileWriter, string route, string stopid, string latitude, string longitude)
        {
            fileWriter.WriteLine(string.Format(@"{0} {1} {2} {3}", route, stopid, latitude, longitude));
        }

        public static void RouteRecordInfo(StreamWriter fileWriter, string route, string stopid, string latitude, string longitude)
        {
            fileWriter.WriteLine(string.Format(@"Route: {0} Stopid: {1} Latitude: {2} Longitude : {3}", route, stopid, latitude, longitude));
        }

        public static void RouteRecordInfoSimulatorFormat(StreamWriter fileWriter, string stopid, string latitude, string longitude)
        {
            fileWriter.WriteLine(string.Format(@"{0} 30 {1} {2} 0.0 4.0", stopid, longitude, latitude));
        }
        #endregion

        #region Stops Outputs
        public static void StopsCompleteReport(StreamWriter fileWriter, int totalStops)
        {
            fileWriter.WriteLine();
            fileWriter.WriteLine(string.Format(@"Total Stops: {0} ", totalStops));  
        }

        public static void StopsRecord(StreamWriter fileWriter, string stopid, string latitude, string longitude)
        {
            fileWriter.WriteLine(string.Format(@"{0} {1} {2}", stopid, latitude, longitude));
        }

        public static void StopsRecordInfo(StreamWriter fileWriter, string stopid, string latitude, string longitude)
        {
            fileWriter.WriteLine(string.Format(@"Stopid: {0} Latitude: {1} Longitude : {2}", stopid, latitude, longitude));
        }
        #endregion
    }
}




