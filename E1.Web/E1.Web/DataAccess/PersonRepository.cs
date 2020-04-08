using System;
using System.Collections.Generic;
using System.Linq;
using E1.Web.Domain;

namespace E1.Web.DataAccess
{
    public interface IPersonRepository
    {
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
            throw new System.NotImplementedException();
        }

        public void AddPerson(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            this.dbContext.Persons.Add(person);
            this.dbContext.SaveChanges();
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