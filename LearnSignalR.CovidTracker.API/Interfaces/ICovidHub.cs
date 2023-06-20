using LearnSignalR.CovidTracker.API.Models;

namespace LearnSignalR.CovidTracker.API.Interfaces
{
    public interface ICovidHub
    {
        Task ReceiveCovidList(List<CovidChart> covidCharts);
    }
}
