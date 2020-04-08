using System.Collections.Generic;
using E1.Web.Domain;

namespace E1.Web.DataAccess
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetGroups();
    }

    public class GroupRepository : IGroupRepository

    {
        public IEnumerable<Group> GetGroups()
        {
            throw new System.NotImplementedException();
        }
    }
}