using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using HousingOffersAPI.Services.ClicksRelated;
using System.IO;

namespace HousingOffersAPI.Services.ScriptRelated
{
    public class RScriptTasksRunner : IRScriptTasksRunner
    {
        public RScriptTasksRunner(string dbConnectionString)
        {
            this.context = new Entities.HousingOffersContext(dbConnectionString);
            this.clicksRepository = new ClicksRepository(this.context);
            this.offersRepository = new OffersRepozitory(this.context);
        }

        private readonly Entities.HousingOffersContext context;
        private readonly IClicksRepository clicksRepository;
        private readonly IOffersRepozitory offersRepository;

        public void RunRScript(string scriptPath)
        {
            Console.WriteLine($"{DateTime.Now}: Running r script");
            var process = new Process();

            process.StartInfo = new ProcessStartInfo()
            {
                FileName = "Rterm",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = scriptPath
            };

            process.Start();
            Console.WriteLine($"{DateTime.Now}: Done running r script");
        }

        public void UpdateScriptCsvs()
        {
            Console.WriteLine($"{DateTime.Now}: Exporting data to csvs");

            List<Task> tasks = new List<Task>();
            File.WriteAllText
                ("offerClicks.csv",
                $"id; datetime{Environment.NewLine}" +
                string.Join(Environment.NewLine, clicksRepository.GetOfferClicks()
                .Select(row => $"{row.OfferId};{row.DateTime}")));

            File.WriteAllTextAsync
                ("userClicks.csv",
                $"id; datetime{Environment.NewLine}" + 
                string.Join(Environment.NewLine, clicksRepository.GetUserClicks()
                .Select(row => $"{row.UserId};{row.DateTime}")));

            File.WriteAllTextAsync
                ("offers.csv",
                $"id; location.x; location.y; priceinpln; userid; propertytype; area; creationdate; description{Environment.NewLine}" +
                string.Join(Environment.NewLine, offersRepository.GetOffers(new Models.OffersRequestContentModel())
                .Select(row =>
                {
                    return $"{ row.Id };{ row.Location.Longitude };{ row.Location.Lattitue };{ row.PriceInPLN };{ row.UserId };{ row.PropertyType };{row.Area};{row.CreationDate};{row.Description}";
                })));

            Console.WriteLine($"{DateTime.Now}: Done exporting data to csvs");
        }
    }
}
