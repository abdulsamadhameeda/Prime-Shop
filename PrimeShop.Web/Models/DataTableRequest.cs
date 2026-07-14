namespace PrimeShop.Web.Models
{
    public class DataTableRequest
    {

        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public Search Search { get; set; }


    }
    public class Search
    {
        public string Value { get; set; }
    }
}

