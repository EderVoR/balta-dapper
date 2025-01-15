using Dapper.Contrib.Extensions;

namespace blog.Models
{
    [Table("tag")]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}