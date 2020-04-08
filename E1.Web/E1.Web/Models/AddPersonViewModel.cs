using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E1.Web.Models
{
    public class AddPersonViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int? GroupId { get; set; }

        public IEnumerable<SelectListItem> Groups { get; set; }
    }
}