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

            File.WriteAllText
                ("userClicks.csv",
                $"id; datetime{Environment.NewLine}" + 
                string.Join(Environment.NewLine, clicksRepository.GetUserClicks()
                .Select(row => $"{row.UserId};{row.DateTime}")));

            File.WriteAllText
                ("offers.csv",
                $"id; location.x; location.y; priceinpln; userid; propertytype; area; creationdate; description{Environment.NewLine}" +
                string.Join(Environment.NewLine, offersRepository.GetOffers(new Models.OffersRequestContentModel())
                .Select(row =>
                {
                    return $"{ row.Id };{ row.Location.Longitude };{ row.Location.Lattitue };{ row.PriceInPLN };{ row.UserId };{ row.PropertyType };{row.Area};{row.CreationDate};{row.Description}";
                })));


            //TODO change script to use files above and remove the linq from below
            string dataIe = "Id; Price; Adress; Area; PropertyType; OfferType; Description; CreationDate; User; Views; Clicks" + Environment.NewLine;

            var lines = context.Offers.Select(offer => offer)
                .ToList().Select(offer => {
                    int id = offer.Id;

                    int offerClicks = context
                        .OfferClicks
                        .Where(offerClick => offerClick.Id == offer.Id).Count();

                    int userClicks = context
                        .UserClicks
                        .Where(userClick => userClick.Id == offer.Id).Count();

                    return $"{id};{offer.PriceInPLN};{offer.Location.Description};{offer.Area};{offer.PropertyType};{offer.OfferType};{offer.Description};{offer.CreationDate};{offer.User.Id};{offerClicks};{userClicks}{Environment.NewLine}";
                });

            dataIe += string.Join("", lines);
            File.WriteAllText("dataIE.csv", dataIe);
            

            Console.WriteLine($"{DateTime.Now}: Done exporting data to csvs");
        }
    }
}
