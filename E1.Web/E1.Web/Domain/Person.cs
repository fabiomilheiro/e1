using System;

namespace E1.Web.Domain
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public DateTime CreatedTimestamp { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}