using LearnSignalR.CovidTracker.API.Contexts;
using LearnSignalR.CovidTracker.API.Dtos;
using LearnSignalR.CovidTracker.API.Hubs;
using LearnSignalR.CovidTracker.API.Interfaces;
using LearnSignalR.CovidTracker.API.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace LearnSignalR.CovidTracker.API.Services
{
    public class CovidService : ICovidService
    {
        private readonly AppDbContext _context;
        private readonly IHubContext<CovidHub> _hubContext;

        public CovidService(AppDbContext context, IHubContext<CovidHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public IQueryable<Covid> GetList()
        {
            return _context.Covids.AsQueryable();
        }

        public async Task<Covid> SaveAsync(CovidDto dto)
        {
            Covid covid = new Covid()
            {
                City = dto.City,
                Count = dto.Count,
                CovidDate = dto.CovidDate,
            };

            _context.Covids.Add(covid);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveCovidList", GetCovidChartList());

            return covid;
        }

        public List<CovidChart> GetCovidChartList()

        {
            List<CovidChart> covidCharts = new List<CovidChart>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "select tarih,[1],[2],[3],[4],[5]  FROM(select[City],[Count], Cast([CovidDate] as date) as tarih FROM Covids) as covidT PIVOT (SUM(Count) For City IN([1],[2],[3],[4],[5]) ) as ptable order by tarih asc";

                command.CommandType = System.Data.CommandType.Text;

                _context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CovidChart cc = new CovidChart();

                        cc.CovidDate = reader.GetDateTime(0).ToShortDateString();

                        Enumerable.Range(1, 5).ToList().ForEach(x =>
                        {
                            if (System.DBNull.Value.Equals(reader[x]))
                            {
                                cc.Counts.Add(0);
                            }
                            else
                            {
                                cc.Counts.Add(reader.GetInt32(x));
                            }
                        });

                        covidCharts.Add(cc);
                    }
                }

                _context.Database.CloseConnection();

                return covidCharts;
            }
        }

    }
}
