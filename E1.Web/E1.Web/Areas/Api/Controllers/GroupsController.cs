using System.Collections.Generic;
using System.Linq;
using E1.Web.DataAccess;
using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace E1.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository groupRepository;

        public GroupsController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GroupViewModel>> GetGroups()
        {
            var groups = this.groupRepository.GetGroups();

            return Ok(groups.Select(g => new GroupViewModel
            {
                Id = g.Id,
                Name = g.Name
            }));
        }
    }
}