namespace E1.Web.Domain
{
    public class SearchPersonCriteria
    {
        public string Name { get; set; }

        public bool SearchByExactName { get; set; }

        public int? GroupId { get; set; }
    }
}