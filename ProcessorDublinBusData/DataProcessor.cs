using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProcessorDublinBusData
{
    public class DataProcessor
    {
        private static IEnumerable<string> RemoveNotApplicableRoutes = new string[]  { "7N", "15N", "29N", "39N", "41N", "42N", "66N", "67N", "69N", "70N", "88N" };

        public static void GenerateListOfStops(DublinBus jsonDublinDataset)
        {
            var filteredbylistOfStops = jsonDublinDataset.results
                .Where(x => x.operators.First().name == "bac")
                .Select(obj =>
                new { obj.latitude, obj.longitude, obj.stopid });

            using (StreamWriter fileWriter = new StreamWriter(@"ListOfStops.txt"))
            {
                filteredbylistOfStops.ToList()
                    .ForEach(s =>
                        FileWriter.StopsRecord(fileWriter, s.stopid, s.latitude, s.longitude));

                FileWriter.StopsCompleteReport(fileWriter, filteredbylistOfStops.Count());
            }
        }

        public static void GenerateListOfRoutes(DublinBus jsonDublinDataset)
        {
            var listOfRoutes = FilterByDublinRoutes(jsonDublinDataset).OrderBy(x => PadNumbers(x)).ToList();
            using (StreamWriter fileWriter = new StreamWriter(@"ListOfRoutes.txt"))
            {
                foreach (var route in listOfRoutes)
                {
                    var filteredbylistOfRoutes = jsonDublinDataset.results
                        .Where(x => x.operators.First().routes.Contains(route) && x.operators.First().name == "bac")
                        .Select(obj =>
                        new { obj.latitude, obj.longitude, obj.stopid, obj.operators.First().routes }).ToArray();

                    filteredbylistOfRoutes.ToList()
                        .ForEach(s =>
                            FileWriter.RouteRecordInfo(fileWriter, route, s.stopid, s.latitude, s.longitude));

                    var total = filteredbylistOfRoutes.ToList().Count;
                    var duplicate = filteredbylistOfRoutes.ToList().GroupBy(x => x.stopid)
                                                                   .Where(g => g.Count() > 1)
                                                                   .Select(y => y.Key).Count();

                    FileWriter.RouteInfo(fileWriter, route, total, duplicate);
                }
                FileWriter.RoutesCompleteReport(fileWriter, listOfRoutes.Count);
            }
        }

        public static void GenerateListOfRoutesForSimulator(DublinBus jsonDublinDataset)
        {
            var listOfRoutes = FilterByDublinRoutes(jsonDublinDataset).OrderBy(x => PadNumbers(x)).ToList();

            using (StreamWriter fileWriter = new StreamWriter(@"ListOfRoutesSimulator.txt"))
            {
                foreach (var route in listOfRoutes)
                {
                    var filteredbylistOfRoutes = jsonDublinDataset.results
                        .Where(x => x.operators.First().routes.Contains(route) && x.operators.First().name == "bac")
                        .Select(obj =>
                        new { obj.latitude, obj.longitude, obj.stopid, obj.operators.First().routes }).ToArray();

                    filteredbylistOfRoutes.ToList()
                        .ForEach(s =>
                            FileWriter.RouteRecordInfoSimulatorFormat(fileWriter, s.stopid, s.latitude, s.longitude));

                    var total = filteredbylistOfRoutes.ToList().Count;
                    var duplicate = filteredbylistOfRoutes.ToList().GroupBy(x => x.stopid)
                                                                   .Where(g => g.Count() > 1)
                                                                   .Select(y => y.Key).Count();

                    FileWriter.RouteInfo(fileWriter, route, total, duplicate);
                }
                FileWriter.RoutesCompleteReport(fileWriter, listOfRoutes.Count);
            }
        }

        private static string PadNumbers(string input)
        {
            return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
        }

        private static List<string> FilterByDublinRoutes(DublinBus jsonDublinDataset)
        {
            var distinctRoutes = new List<string>();

            foreach (var result in jsonDublinDataset.results)
            {
                foreach (var op in result.operators)
                {
                    if (op.name == "bac")
                    {
                        foreach (var route in op.routes)
                        {
                            distinctRoutes.Add(route);
                        }
                    }
                }
            }
            distinctRoutes = distinctRoutes.Distinct().ToList();

            var removedNARoutes = RemoveNotApplicableRoutes;
            return distinctRoutes.Where(x => !removedNARoutes.ToList().Contains(x.ToUpper())).ToList();
        }  
    }
}