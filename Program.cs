using balta_dapper.Models;
using Dapper;
using Npgsql;

var connection = new NpgsqlConnection(
    connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;");

using(var connect = new NpgsqlCommand())
{    
    //InsertCategory(connection);
    //UpdateCategory(connection);
    ListCategory(connection);
}

static void ListCategory(NpgsqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT ID, NAME, SLUG FROM CATEGORY ORDER BY ID");

    foreach(var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Name}/{category.Slug}");
    }
}

static void InsertCategory(NpgsqlConnection connection)
{
    var newCategory = new Category
    {
        Name = "Angular",
        Slug = "angular"    
    };

    var insertDado = @"insert into category (name, slug) 
                    VALUES (@Name, @Slug)";

    var rows = connection.Execute(insertDado, new {
        Name = newCategory.Name,
        Slug = newCategory.Slug
    });

    Console.WriteLine($"{rows} linhas afetadas");
}

static void UpdateCategory(NpgsqlConnection connection)
{
    var updateQuery = "update category set name = @name, slug = @slug where id = 12";

    var rows = connection.Execute(updateQuery, new {
        name = "Amazon AWS",
        slug = "cloud Amazon AWS"
    });

    Console.WriteLine($"{rows} linhas afetadas");
}