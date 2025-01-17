using Npgsql;
using Dapper.FluentMap;
using Blog.Mappings;
using Blog.Screens.TagScreens;

FluentMapper.Initialize(config => 
{
    config.AddMap(new UsuarioMap());
});

var connectionPost = new NpgsqlConnection(
    connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;"
    //connectionString: "User ID=postgres; Password=3d3r3001; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;"
);

connectionPost.Open();

Load();

connectionPost.Close();
Console.ReadKey();

static void Load()
{
    Console.Clear();
    Console.WriteLine("Meu Blog");
    Console.WriteLine("-----------------");
    Console.WriteLine("O que deseja fazer?");
    Console.WriteLine();
    Console.WriteLine("1 - Gestão de usuário");
    Console.WriteLine("2 - Gestão de perfil");
    Console.WriteLine("3 - Gestão de categoria");
    Console.WriteLine("4 - Gestão de Tag");
    Console.WriteLine("5 - Vincular Perfil/Usuário");
    Console.WriteLine("6 - Vincular Post/Tag");
    Console.WriteLine("7 - Relatórios");
    Console.WriteLine();
    var option = short.Parse(Console.ReadLine());

    switch(option)
    {
        case 4:
            MenuTagScreen.Load();
            break;
        default: Load(); break;
    }

}