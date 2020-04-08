namespace E1.Web.Extensions
{
    public static class StringExtensions
    {
        public static int ToInteger(this object source, int defaultValue = 0)
        {
            if (source == null)
            {
                return defaultValue;
            }

            int.TryParse(source.ToString(), out var result);
            
            return result;
        }
    }
}