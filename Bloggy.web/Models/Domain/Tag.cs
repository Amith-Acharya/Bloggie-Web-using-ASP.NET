namespace Bloggy.web.Models.Domain
{
    public class Tag
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Displayname { get; set; }
        public ICollection<BlogPost> BlogPost { get; set; }
    }
}
