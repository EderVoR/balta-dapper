using Blog.Models;
using Npgsql;
using Dapper.FluentMap;
using Blog.Mappings;
using blog.Repositories;

FluentMapper.Initialize(config => 
{
    config.AddMap(new UsuarioMap());
});

var connectionPost = new NpgsqlConnection(
    connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;"
    //connectionString: "User ID=postgres; Password=3d3r3001; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;"
);

connectionPost.Open();

ReadUsers(connectionPost);
ReadGrupos(connectionPost);

connectionPost.Close();

static void ReadUsers(NpgsqlConnection connectionPost)
{
    var repository = new Repository<Usuario>(connectionPost);
    var usuarios = repository.Get();

    foreach(var item in usuarios)
        Console.WriteLine(item.Name);
}

static void ReadGrupos(NpgsqlConnection connectionPost)
{
    var repository = new Repository<Grupo>(connectionPost);
    var grupos = repository.Get();

    foreach(var item in grupos)
        Console.WriteLine(item.Name);
}