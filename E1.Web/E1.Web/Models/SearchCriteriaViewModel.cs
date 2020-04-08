namespace E1.Web.Models
{
    public class SearchCriteriaViewModel
    {
        public string Name { get; set; }

        public bool SearchByExactName { get; set; }

        public int? GroupId { get; set; }
    }
}