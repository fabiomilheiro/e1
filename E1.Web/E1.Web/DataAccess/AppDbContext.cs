using E1.Web.Domain;
using Microsoft.EntityFrameworkCore;

namespace E1.Web.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        public DbSet<Person> Persons { get; set; }
    }
}