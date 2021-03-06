﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E1.Web.DataAccess;
using E1.Web.Extensions;
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

        public IViewComponentResult Invoke()
        {
            var groupId = Request.Query["groupId"].ToInteger();

            return View(new PersonSearchViewComponentModel
            {
                ExactName = Request.Query["exactName"].ToString(),
                PartialName = Request.Query["partialName"].ToString(),
                GroupId = groupId,
                Groups = 
                    new [] { new SelectListItem("(Select)", "") }
                    .Union(
                        this.groupRepository
                            .GetGroups()
                            .Select(g => new SelectListItem(g.Name, g.Id.ToString(), g.Id == groupId))
                            .ToArray())
            });
        }
    }
}