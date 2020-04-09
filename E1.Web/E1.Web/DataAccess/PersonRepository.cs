using System;
using System.Collections.Generic;
using System.Linq;
using E1.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace E1.Web.DataAccess
{
    public interface IPersonRepository
    {
        bool Exists(string exactName);

        IEnumerable<Person> Search(SearchPersonCriteria criteria);

        Person GetPerson(int id);

        void AddPerson(Person person);
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly AppDbContext dbContext;

        public PersonRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool Exists(string exactName)
        {
            return this.dbContext.Persons.Any(p => p.Name == exactName);
        }

        public IEnumerable<Person> Search(SearchPersonCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            if (ShouldUseExactName(criteria) && ShouldUsePartialName(criteria))
            {
                throw new ArgumentException(
                    "Cannot use partial and exact search at the same time.",
                    nameof(criteria));
            }

            var query = this.dbContext.Persons.AsQueryable();

            if (ShouldUseExactName(criteria))
            {
                query = query.Where(p => p.Name == criteria.ExactName);
            }
            
            if (ShouldUsePartialName(criteria))
            {
                query = query.Where(p => p.Name.Contains(criteria.PartialName));
            }

            if (criteria.GroupId.HasValue)
            {
                query = query.Where(p => p.GroupId == criteria.GroupId.Value);
            }

            return query.ToArray();
        }

        public Person GetPerson(int id)
        {
            var person = this.dbContext.Persons.FirstOrDefault(p => p.Id == id);

            return person;
        }

        public void AddPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            var personProxy = this.dbContext.CreateProxy<Person>();
            personProxy.Name = person.Name;
            personProxy.CreatedTimestamp = person.CreatedTimestamp;
            personProxy.GroupId = person.GroupId;
            
            this.dbContext.Persons.Add(personProxy);
            this.dbContext.SaveChanges();

            person.Id = personProxy.Id;
        }

        private static bool ShouldUseExactName(SearchPersonCriteria criteria)
        {
            return !string.IsNullOrEmpty(criteria.ExactName);
        }

        private static bool ShouldUsePartialName(SearchPersonCriteria criteria)
        {
            return !string.IsNullOrEmpty(criteria.PartialName);
        }
    }
}