using Blog.Models;
using Dapper.Contrib.Extensions;
using Npgsql;
using Dapper.FluentMap;
using Blog.Mappings;

FluentMapper.Initialize(config => 
{
    config.AddMap(new UsuarioMap());
});

var connectionPost = new NpgsqlConnection(
    connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;");

//ReadUsers(connectionPost);
//ReadUser(connectionPost);
CreateUser(connectionPost);

static void ReadUsers(NpgsqlConnection connectionPost)
{
    using (var connection = new NpgsqlCommand())
    {
        var users = connectionPost.GetAll<Usuario>();

        foreach (var item in users)
            Console.WriteLine($"{item.Name}");
    }
}

static void ReadUser(NpgsqlConnection connectionPost)
{
    using (var connection = new NpgsqlCommand())
    {
        var users = connectionPost.Get<Usuario>(1);

        Console.WriteLine($"Read: {users.Name}");
    }
}

static void CreateUser(NpgsqlConnection connectionPost)
{
    var newUser = new Usuario
    {
        Bio = "Aqui estou eu",
        Email = "aqui@teste",
        Image = "https>//....",
        Name = "Aluno de teste",
        PasswordHash = "87sfds5gsfdg56",
        Slug = "aluno-de-teste"
    };

    using (var connection = new NpgsqlCommand())
    {
        var users = connectionPost.Insert<Usuario>(newUser);

        Console.WriteLine($"Create: {users}");
    }
}