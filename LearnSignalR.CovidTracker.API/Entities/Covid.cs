using LearnSignalR.CovidTracker.API.Enums;

namespace LearnSignalR.CovidTracker.API.Models
{

    public class Covid
    {
        public int Id { get; set; }
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
