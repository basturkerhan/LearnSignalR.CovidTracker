using LearnSignalR.CovidTracker.API.Dtos;
using LearnSignalR.CovidTracker.API.Enums;
using LearnSignalR.CovidTracker.API.Interfaces;
using LearnSignalR.CovidTracker.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearnSignalR.CovidTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CovidsController : ControllerBase
    {
        private readonly ICovidService _service;

        public CovidsController(ICovidService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> SaveAsync(CovidDto dto)
        {
            Covid covid = await _service.SaveAsync(dto);
            var charts = _service.GetCovidChartList();
            return Ok(charts);
        }

        [HttpGet]
        public IActionResult InitializeCommit()
        {
            Random rnd = new();
            Enumerable.Range(1, 10).ToList().ForEach(x =>
            {
                foreach (ECity item in Enum.GetValues(typeof(ECity)))
                {
                    CovidDto newCovid = new()
                    {
                        City = item,
                        Count = rnd.Next(100, 1000),
                        CovidDate = DateTime.Now.AddDays(x)
                    };
                    _service.SaveAsync(newCovid).Wait();
                    System.Threading.Thread.Sleep(1000);
                }
            });

            return Ok();
        }

    }
}
