using System;
using System.IO;
using Newtonsoft.Json;

namespace ProcessorDublinBusData
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\", @"\Dataset\jsonDublinDataset.json")))
            {
                string json = file.ReadToEnd();
                DublinBus jsonDublinDataset = JsonConvert.DeserializeObject<DublinBus>(json);

                DataProcessor.GenerateListOfStops(jsonDublinDataset);
                DataProcessor.GenerateListOfRoutes(jsonDublinDataset);
                DataProcessor.GenerateListOfRoutesForSimulator(jsonDublinDataset);
            }
        }
    }
}



