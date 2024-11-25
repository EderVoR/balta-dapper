namespace balta_dapper.Models
{
    public class PostTags
    {
        public PostTags()
        {
            Tags = new List<Tags>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public List<Tags> Tags { get; set; }
    }
}