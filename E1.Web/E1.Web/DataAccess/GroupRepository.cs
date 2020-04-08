using System.Collections.Generic;
using System.Linq;
using E1.Web.Domain;

namespace E1.Web.DataAccess
{
    public interface IGroupRepository
    {
        IEnumerable<Group> GetGroups();
    }

    public class GroupRepository : IGroupRepository

    {
        private readonly AppDbContext dbContext;

        public GroupRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Group> GetGroups()
        {
            return this.dbContext.Groups.ToArray();
        }
    }
}