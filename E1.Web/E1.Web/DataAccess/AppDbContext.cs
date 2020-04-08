using E1.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace E1.Web.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Person> Persons { get; set; }
    }
}