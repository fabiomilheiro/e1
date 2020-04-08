using System;

namespace E1.Web.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime CreatedTimestamp { get; set; }
     
        public string GroupName { get; set; }
    }
}