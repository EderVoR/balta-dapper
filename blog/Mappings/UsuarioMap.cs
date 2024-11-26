using Blog.Models;
using Dapper.FluentMap.Mapping;

namespace Blog.Mappings
{
    public class UsuarioMap : EntityMap<Usuario>
    {
        public UsuarioMap()
        {
            Map(p => p.Name).ToColumn("name", caseSensitive: false);
            Map(p => p.Bio).ToColumn("bio");
            Map(p => p.Email).ToColumn("email");
            Map(p => p.Id).ToColumn("id");
            Map(p => p.Image).ToColumn("image");
            Map(p => p.PasswordHash).ToColumn("passwordhash");
            Map(p => p.Slug).ToColumn("slug");
        }
    }
}