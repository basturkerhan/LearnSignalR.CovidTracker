using LearnSignalR.CovidTracker.API.Interfaces;
using LearnSignalR.CovidTracker.API.Models;
using Microsoft.AspNetCore.SignalR;

namespace LearnSignalR.CovidTracker.API.Hubs
{
    public class CovidHub : Hub<ICovidHub>
    {
        private readonly ICovidService _covidService;

        public CovidHub(ICovidService covidService)
        {
            _covidService = covidService;
        }

        public async Task GetCovidListAsync()
        {
            List<CovidChart> covidCharts = _covidService.GetCovidChartList();
            await Clients.All.ReceiveCovidList(covidCharts);
        }



    }
}
