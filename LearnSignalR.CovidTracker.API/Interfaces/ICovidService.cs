using LearnSignalR.CovidTracker.API.Dtos;
using LearnSignalR.CovidTracker.API.Models;

namespace LearnSignalR.CovidTracker.API.Interfaces
{
    public interface ICovidService
    {
        IQueryable<Covid> GetList();
        List<CovidChart> GetCovidChartList();
        Task<Covid> SaveAsync(CovidDto dto);
    }
}
