using ASquare.WindowsTaskScheduler;
using ASquare.WindowsTaskScheduler.Models;
using HousingOffersAPI.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.TaskRelated
{
    public class BackgroundTaskScheduler : IBackgroundTaskScheduler
    { 
        public async Task Schedule(Action task, TimeSpan delay)
        {
            await Task.Delay(delay);
            await Task.Run(task);

            Schedule(task, delay);
        }
        public async Task Schedule(Task task, TimeSpan delay)
        {
            await Task.Delay(delay);
            task.Start();
            await task;

            Schedule(task, delay);
        }
    }
}
