using System;
using System.Linq;
using E1.Web.DataAccess;
using E1.Web.Domain;
using E1.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace E1.Web.Controllers
{
    [Route("persons")]
    public class PersonController : Controller
    {
        private readonly IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public IActionResult Search(SearchCriteriaViewModel criteria)
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
                GroupName = p.Group.Name
            });

            return View(new PersonSearchViewModel
            {
                Persons = personViewModels
            });
        }
    }
}