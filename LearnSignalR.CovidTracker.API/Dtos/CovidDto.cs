using LearnSignalR.CovidTracker.API.Enums;

namespace LearnSignalR.CovidTracker.API.Dtos
{
    public class CovidDto
    {
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
