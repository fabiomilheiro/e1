using E1.Web.Domain;

namespace E1.Web.DataAccess
{
    public interface IPersonRepository
    {
        Person GetPerson(int id);

        Person AddPerson(Person person);
    }

    public class PersonRepository : IPersonRepository
    {
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