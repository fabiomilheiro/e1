using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E1.Web.DataAccess;
using E1.Web.Domain;
using E1.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E1.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonViewModel>> SearchPerson([FromQuery] SearchCriteriaViewModel criteria)
        {
            var persons = this.personRepository.Search(new SearchPersonCriteria
            {
                ExactName = criteria.ExactName,
                PartialName = criteria.PartialName,
                GroupId = criteria.GroupId
            });

            return Ok(persons.Select(p => new PersonViewModel
            {
                Id = p.Id,
                Name = p.Name,
                CreatedTimestamp = p.CreatedTimestamp,
                GroupName = p.Group.Name
            }));
        }
    }
}