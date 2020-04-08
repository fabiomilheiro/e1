using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E1.Web.Models
{
    public class PersonSearchViewComponentModel
    {
        public string PartialName { get; set; }

        public string ExactName { get; set; }

        public int? GroupId { get; set; }
        
        public IEnumerable<SelectListItem> Groups { get; set; }
    }
}