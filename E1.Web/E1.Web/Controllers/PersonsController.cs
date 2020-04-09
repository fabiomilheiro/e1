
using System;
using System.Collections.Generic;
using System.Linq;
using E1.Web.DataAccess;
using E1.Web.Domain;
using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E1.Web.Controllers
{
    [Route("persons")]
    public class PersonsController : Controller
    {
        private readonly IPersonRepository personRepository;
        private readonly IGroupRepository groupRepository;

        public PersonsController(
            IPersonRepository personRepository,
            IGroupRepository groupRepository)
        {
            this.personRepository = personRepository;
            this.groupRepository = groupRepository;
        }

        [HttpGet]
        public IActionResult Index([FromQuery]SearchCriteriaViewModel criteria)
        {
            var persons = this.personRepository.Search(new SearchPersonCriteria
            {
                PartialName = criteria.PartialName,
                ExactName = criteria.ExactName,
                GroupId = criteria.GroupId
            });

            var personViewModels = persons.Select(p => new PersonViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CreatedTimestamp = p.CreatedTimestamp,
                GroupName = p.Group.Name
            });

            return View(new PersonSearchViewModel
            {
                Persons = personViewModels.ToArray()
            });
        }

        [HttpGet("add")]
        public IActionResult Add()
        {
            return View(new AddPersonViewModel
            {
                Groups =
                    GetGroupSelectListItems(null)
            });
        }

        [HttpPost("add")]
        public IActionResult Add([FromForm] AddPersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Groups = GetGroupSelectListItems(model.GroupId);
                return View(model);
            }

            this.personRepository.AddPerson(new Person
            {
                Name = model.Name,
                CreatedTimestamp = DateTime.UtcNow,
                GroupId = model.GroupId.GetValueOrDefault()
            });

            this.TempData["Success"] = "The person was created successfully.";
            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetGroupSelectListItems(int? groupId)
        {
            return new[] { new SelectListItem("(Select)", "") }
                .Union(
                    this.groupRepository
                        .GetGroups()
                        .Select(g => new SelectListItem(g.Name, g.Id.ToString(), g.Id == groupId))
                        .ToArray());
        }
    }
}