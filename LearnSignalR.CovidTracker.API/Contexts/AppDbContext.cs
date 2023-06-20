using LearnSignalR.CovidTracker.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnSignalR.CovidTracker.API.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public virtual DbSet<Covid> Covids { get; set; }
    }
}
