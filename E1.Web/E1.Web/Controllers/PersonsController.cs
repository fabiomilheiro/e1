using System;
using System.Linq;
using E1.Web.DataAccess;
using E1.Web.Domain;
using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace E1.Web.Controllers
{
    [Route("persons")]
    public class PersonsController : Controller
    {
        private readonly IPersonRepository personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult Index(SearchCriteriaViewModel criteria)
        {
            var persons = this.personRepository.Search(new SearchPersonCriteria
            {
                ExactName = criteria.Name,
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
    }
}