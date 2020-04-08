namespace E1.Web.Domain
{
    public class SearchPersonCriteria
    {
        public string PartialName { get; set; }

        public string ExactName { get; set; }

        public int? GroupId { get; set; }
    }
}