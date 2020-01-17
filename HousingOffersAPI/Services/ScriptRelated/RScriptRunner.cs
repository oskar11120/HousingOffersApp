using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HousingOffersAPI.Services.ScriptRelated
{
    public static class RScriptRunner
    {
        public static void Run(string scriptPath)
        {
            var process = new Process();

            process.StartInfo = new ProcessStartInfo()
            {
                FileName = "Rterm",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = scriptPath
            };

            process.Start();
        }
    }
}
