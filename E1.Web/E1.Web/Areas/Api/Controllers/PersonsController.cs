﻿using System;
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

        [HttpPost]
        public ActionResult<PersonViewModel> AddPerson([FromBody] AddPersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = new Person
            {
                Name = model.Name,
                CreatedTimestamp = DateTime.UtcNow,
                GroupId = model.GroupId.GetValueOrDefault()
            };

            this.personRepository.AddPerson(person);

            person = this.personRepository.GetPerson(person.Id);

            return Ok(new PersonViewModel
            {
                Id = person.Id,
                Name = person.Name,
                GroupName = person.Group.Name,
                CreatedTimestamp = person.CreatedTimestamp
            });
        }
    }
}