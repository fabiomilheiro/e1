using System.Linq;
using System.Threading.Tasks;
using E1.Web.DataAccess;
using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E1.Web.Components
{
    public class PersonSearchViewComponent : ViewComponent
    {
        private readonly IGroupRepository groupRepository;

        public PersonSearchViewComponent(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        public IViewComponentResult Invoke(string exactName, string partialName, int? groupId)
        {
            return View(new PersonSearchViewComponentModel
            {
                ExactName = exactName,
                PartialName = partialName,
                GroupId = groupId,
                Groups = this.groupRepository.GetGroups().Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = g.Id == groupId
                })
            });
        }
    }
}