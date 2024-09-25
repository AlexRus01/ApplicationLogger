using Microsoft.EntityFrameworkCore;

namespace LogApplication
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnet-53bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        public DbSet<Car> Cars { get; set; }



    }


    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }

        public int maxSpeed { get; set; }
        public string color { get; set; }
    }
}
