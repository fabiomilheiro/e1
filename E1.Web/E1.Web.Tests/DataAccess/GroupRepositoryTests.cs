using E1.Web.DataAccess;
using E1.Web.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace E1.Web.Tests.DataAccess
{
    public class GroupRepositoryTests
    {
        private AppDbContext dbContext;

        private GroupRepository sut;

        public GroupRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>(new DbContextOptions<AppDbContext>());
            builder.UseSqlite("DataSource=:memory:");

            this.dbContext = new AppDbContext(builder.Options);
            this.dbContext.Database.OpenConnection();
            this.dbContext.Database.EnsureCreated();

            this.sut = new GroupRepository(this.dbContext);
        }

        [Fact]
        public void GetGroups_Always_ReturnsAllGroups()
        {
            var groupA = new Group { Name = "A" };
            var groupB = new Group { Name = "B" };
            var groupC = new Group { Name = "C" };
            this.dbContext.Groups.AddRange(groupA, groupB, groupC);
            this.dbContext.SaveChanges();

            var result = this.sut.GetGroups();

            result.Should().BeEquivalentTo(groupA, groupB, groupC);
        }
    }
}