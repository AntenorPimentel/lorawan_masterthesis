using System.Collections.Generic;

namespace ProcessorDublinBusData
{
    public class DublinBus
    {
        public string errorcode { get; set; }
        public string errormessage { get; set; }
        public int numberofresults { get; set; }
        public string timestamp { get; set; }
        public List<Result> results { get; set; }
    }

    public class Result
    {
        public string stopid { get; set; }
        public string displaystopid { get; set; }
        public string shortname { get; set; }
        public string shortnamelocalized { get; set; }
        public string fullname { get; set; }
        public string fullnamelocalized { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string lastupdated { get; set; }
        public IList<Operator> operators { get; set; }
    }

    public class Operator
    {
        public string name { get; set; }
        public IList<string> routes { get; set; }
    }
}

