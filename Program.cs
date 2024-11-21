using balta_dapper.Models;
using Dapper;
using Npgsql;

var connection = new NpgsqlConnection(
    connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;");
connection.Open();

using(var connect = new NpgsqlCommand())
{

    var categories = connection.Query<Category>("SELECT ID, NAME, SLUG FROM CATEGORY");

    foreach(var category in categories)
    {
        Console.WriteLine($"{category.Id} - {category.Name}/{category.Slug}");
    }
}