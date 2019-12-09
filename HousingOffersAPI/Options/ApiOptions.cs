using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Options
{
    public class ApiOptions
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
        public UsersControllerOptions UsersControllerOptions {get; set;}
        public OffersControllerOptions OffersControllerOptions { get; set; }
    }

    public class UsersControllerOptions
    {
        public Dictionary<string, string> SecurityKeys { get; set; }
    }
    public class OffersControllerOptions
    {
        public Dictionary<string, List<string>> AllowedValues {get; set;}
    }
}
