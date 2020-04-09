using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using E1.Web.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E1.Web.Models
{
    public class AddPersonViewModel
    {
        [Required]
        [ValidateUniqueName]
        public string Name { get; set; }

        [Required]
        public int? GroupId { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }
    }
}