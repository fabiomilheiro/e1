using System;
using System.Collections.Generic;
using E1.Web.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace E1.Web.Tests.DataAccess
{
    public class PersonRepositoryTests
    {
        private AppDbContext dbContext;
        
        private PersonRepository sut;

        public PersonRepositoryTests()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>(
                new DbContextOptions<AppDbContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
            builder.UseInMemoryDatabase("PersonRepositoryTestsDatabase");

            this.dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>(new DbContextOptions<AppDbContext>()).Options);
            this.sut = new PersonRepository(this.dbContext);
        }

        [Fact]
        public void Search_FiltersByGroup_ReturnsPersonsInGroup()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Search_FiltersByName_ReturnsPersonsWithExactMatch()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Search_FiltersByNamePartial_ReturnsPersonsWithPartialMatch()
        {
            throw new NotImplementedException();
        }
    }
}