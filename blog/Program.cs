using Blog.Models;
using Dapper.Contrib.Extensions;
using Npgsql;
using Dapper.FluentMap;
using Blog.Mappings;
using Dapper;

FluentMapper.Initialize(config => 
{
    config.AddMap(new UsuarioMap());
});

var connectionPost = new NpgsqlConnection(
    //connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;"
    connectionString: "User ID=postgres; Password=3d3r3001; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;"
);

//ReadUsers(connectionPost);
//ReadUser(connectionPost);
//CreateUser(connectionPost);
//UpdateUser(connectionPost);
DeleteUser(connectionPost);

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
    // var newUser = new Usuario
    // {
    //     Bio = "Aqui estou eu",
    //     Email = "aqui@teste",
    //     Image = "https>//....",
    //     Name = "Aluno de teste",
    //     PasswordHash = "87sfds5gsfdg56",
    //     Slug = "aluno-de-teste"
    // };

    using (var connection = new NpgsqlCommand())
    {
        //var users = connectionPost.Insert<Usuario>(newUser);

        connectionPost.Execute(@"INSERT INTO usuario (id, bio, email, image, name, password_hash, slug) VALUES (
        @Id, @Bio, @Email, @Image, @Name, @Password, @Slug)",
        new {
            @Id = 1,
            @Bio = "Aqui estou eu",
            @Email = "aqui@teste",
            @Image = "https>//....",
            @Name = "Aluno de teste",
            @Password = "87sfds5gsfdg56",
            @Slug = "aluno-de-teste"
        });

        Console.WriteLine($"Usuario criado com sucesso!");
    }
}

static void UpdateUser(NpgsqlConnection connectionPost)
{
    using (var connection = new NpgsqlCommand())
    {
        connectionPost.Execute(@"UPDATE usuario 
                                 set bio = @Bio
                                 where id = @Id",
        new {
            @Id = 1,
            @Bio = "Aqui estou eu"
        });

        Console.WriteLine($"Usuario atualizado com sucesso!");
    }
}

static void DeleteUser(NpgsqlConnection connectionPost)
{
    using (var connection = new NpgsqlCommand())
    {
        connectionPost.Execute(@"delete from usuario where id = @Id",new 
        {
            @Id = 1
        });
        Console.WriteLine($"Usuario deletado com sucesso!");
    }
}