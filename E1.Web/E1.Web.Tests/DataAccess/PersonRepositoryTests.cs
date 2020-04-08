using System;
using System.Collections.Generic;
using E1.Web.DataAccess;
using E1.Web.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace E1.Web.Tests.DataAccess
{
    public class PersonRepositoryTests
    {
        private AppDbContext dbContext;

        private PersonRepository sut;
        private Group administratorsGroup;
        private Group teachersGroup;

        public PersonRepositoryTests()
        {
            this.administratorsGroup = new Group { Name = "Administrators" };
            this.teachersGroup = new Group { Name = "Teachers" };

            var builder = new DbContextOptionsBuilder<AppDbContext>(
                new DbContextOptions<AppDbContext>(new Dictionary<Type, IDbContextOptionsExtension>()));
            builder.UseInMemoryDatabase("PersonRepositoryTestsDatabase");

            this.dbContext = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>(new DbContextOptions<AppDbContext>()).Options);
            this.sut = new PersonRepository(this.dbContext);
        }

        [Fact]
        public void Search_FiltersByGroup_ReturnsPersonsInGroup()
        {
            var administrator = new Person { Name = "X", Group = administratorsGroup };
            var teacher = new Person { Name = "Y", Group = this.teachersGroup };
            this.dbContext.Groups.Add(administratorsGroup);
            this.dbContext.Persons.AddRange(new[] { administrator, teacher });
            this.dbContext.SaveChanges();

            var result = this.sut.Search(new SearchPersonCriteria
            {
                GroupId = administratorsGroup.Id
            });

            result.Should().BeEquivalentTo(administrator);
        }

        [Fact]
        public void Search_FiltersByExactName_ReturnsPersonsWithExactMatch()
        {
            var johnSmith = new Person { Name = "John", Group = administratorsGroup };
            var johnSomethingElse = new Person { Name = "John Something Else", Group = this.administratorsGroup };
            this.dbContext.Groups.Add(administratorsGroup);
            this.dbContext.Persons.AddRange(new[] { johnSmith, johnSomethingElse });
            this.dbContext.SaveChanges();

            var result = this.sut.Search(new SearchPersonCriteria
            {
                SearchByExactName = true,
                Name = "john"
            });

            result.Should().BeEquivalentTo(johnSmith);
        }

        [Fact]
        public void Search_FiltersByNamePartial_ReturnsPersonsWithPartialMatch()
        {
            var johnSmith = new Person { Name = "John Smith", Group = administratorsGroup };
            var johnSomethingElse = new Person { Name = "John Something Else", Group = this.administratorsGroup };
            this.dbContext.Groups.Add(administratorsGroup);
            this.dbContext.Persons.AddRange(new[] { johnSmith, johnSomethingElse });
            this.dbContext.SaveChanges();

            var result = this.sut.Search(new SearchPersonCriteria
            {
                SearchByExactName = false,
                Name = "john"
            });

            result.Should().BeEquivalentTo(johnSmith, johnSomethingElse);
        }
    }
}