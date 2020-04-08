﻿using System;
using System.Collections.Generic;
using E1.Web.DataAccess;
using E1.Web.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Xunit;

namespace E1.Web.Tests.DataAccess
{
    public class PersonRepositoryTests : IDisposable
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
            builder.UseSqlite("DataSource=:memory:");

            this.dbContext = new AppDbContext(builder.Options);
            this.dbContext.Database.OpenConnection();
            this.dbContext.Database.EnsureCreated();

            this.sut = new PersonRepository(this.dbContext);
        }

        [Fact]
        public void Search_NullCriteria_Throws()
        {
            Action action = () => this.sut.Search(null);

            action.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("criteria");
        }

        [Fact]
        public void Search_FiltersOnlyByGroup_ReturnsPersonsInGroup()
        {
            var administrator = new Person { Name = "X", Group = administratorsGroup };
            var teacher = new Person { Name = "Y", Group = this.teachersGroup };
            this.dbContext.Groups.Add(administratorsGroup);
            this.dbContext.Persons.AddRange(administrator, teacher);
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
            this.dbContext.Persons.AddRange(johnSmith, johnSomethingElse);
            this.dbContext.SaveChanges();

            var result = this.sut.Search(new SearchPersonCriteria
            {
                ExactName = "John"
            });

            result.Should().BeEquivalentTo(johnSmith);
        }

        [Fact]
        public void Search_FiltersByNamePartial_ReturnsPersonsWithPartialMatch()
        {
            var johnSmith = new Person { Name = "John Smith", Group = administratorsGroup };
            var johnSomethingElse = new Person { Name = "John Something Else", Group = this.administratorsGroup };
            var mary = new Person { Name = "Mary Smith", Group = this.administratorsGroup };
            this.dbContext.Groups.Add(administratorsGroup);
            this.dbContext.Persons.AddRange(johnSmith, johnSomethingElse, mary);
            this.dbContext.SaveChanges();

            var result = this.sut.Search(new SearchPersonCriteria
            {
                PartialName = "Smith"
            });

            result.Should().BeEquivalentTo(johnSmith, mary);
        }

        [Fact]
        public void Search_CombinesSearchParameters_ReturnsIntersectionOfResults()
        {
            var johnSmith = new Person { Name = "John Smith", Group = administratorsGroup };
            var johnSomethingElse = new Person { Name = "John Something Else", Group = this.administratorsGroup };
            var mary = new Person { Name = "Mary Smith", Group = this.teachersGroup };
            this.dbContext.Groups.Add(administratorsGroup);
            this.dbContext.Persons.AddRange(johnSmith, johnSomethingElse, mary);
            this.dbContext.SaveChanges();

            var result = this.sut.Search(new SearchPersonCriteria
            {
                PartialName = "Smith",
                GroupId = this.teachersGroup.Id
            });

            result.Should().BeEquivalentTo(mary);
        }

        [Fact]
        public void Search_PartialAndExactName_Throws()
        {
            Action action = () => this.sut.Search(new SearchPersonCriteria
            {
                PartialName = "John",
                ExactName = "John Smith"
            });

            action.Should().Throw<ArgumentException>().Which.ParamName.Should().Be("criteria");
        }

        public void Dispose()
        {
            dbContext?.Dispose();
        }
    }
}