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

        Person AddPerson(Person person);
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

            var query = this.dbContext.Persons.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.ExactName))
            {
                query = query.Where(p => p.Name == criteria.ExactName);
            }
            
            if (!string.IsNullOrEmpty(criteria.PartialName))
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

        public Person AddPerson(Person person)
        {
            throw new System.NotImplementedException();
        }
    }
}