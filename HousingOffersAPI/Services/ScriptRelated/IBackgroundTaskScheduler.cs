using System;
using System.Threading.Tasks;

namespace HousingOffersAPI.Services.TaskRelated
{
    public interface IBackgroundTaskScheduler
    {
        Task Schedule(Action task, TimeSpan delay);
        Task Schedule(Task task, TimeSpan delay);
    }
}