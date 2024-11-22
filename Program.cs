using balta_dapper.Models;
using Dapper;
using Npgsql;

var connection = new NpgsqlConnection(
    connectionString: "User ID=postgres; Password=admin123; Server=localhost; Database=blog; Pooling=true; TrustServerCertificate=true;");

using (var connect = new NpgsqlCommand())
{
    //InsertCategory(connection);
    //InsertManyCategory(connection);
    //UpdateCategory(connection);
    //ListCategory(connection);
    //ExecutarView(connection);
    //OneToOne(connection);
    OneToMany(connection);
}

static void ListCategory(NpgsqlConnection connection)
{
    var categories = connection.Query<Category>("SELECT ID, NAME, SLUG FROM CATEGORY ORDER BY ID");

    foreach (var category in categories)
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

    var rows = connection.Execute(insertDado, new
    {
        Name = newCategory.Name,
        Slug = newCategory.Slug
    });

    Console.WriteLine($"{rows} linhas afetadas");
}

static void UpdateCategory(NpgsqlConnection connection)
{
    var updateQuery = "update category set name = @name, slug = @slug where id = 12";

    var rows = connection.Execute(updateQuery, new
    {
        name = "Amazon AWS",
        slug = "cloud Amazon AWS"
    });

    Console.WriteLine($"{rows} linhas afetadas");
}

static void InsertManyCategory(NpgsqlConnection connection)
{
    var newCategory = new Category
    {
        Name = "Cloud",
        Slug = "cloudf"
    };

    var newCategory2 = new Category
    {
        Name = "Javascript",
        Slug = "javascript"
    };

    var insertDado = @"insert into category (name, slug) 
                    VALUES (@Name, @Slug)";

    var rows = connection.Execute(insertDado, new[] {
       new{
        Name = newCategory.Name,
        Slug = newCategory.Slug
       },
       new{
        Name = newCategory2.Name,
        Slug = newCategory2.Slug
       }
    });

    Console.WriteLine($"{rows} linhas afetadas");
}

static void ExecutarView(NpgsqlConnection connection)
{
    var sql = "select * from listaCategorias";
    var courses = connection.Query(sql);

    foreach(var item in courses)
        Console.WriteLine($"Id: {item.id}, Name: {item.name}");
}

static void OneToOne(NpgsqlConnection connection)
{
    var sql = @"SELECT p.id, p.title, c.id categoryid, c.name, 
            c.slug  FROM post p join CATEGORY c on p.categoryid = c.id";
    var items = connection.Query<PostItem, Category, PostItem>(sql,
        (postItem, cat) => {
            postItem.Category = cat;
            return postItem;
        }, splitOn: "categoryid");

    foreach(var item in items)
    {
        Console.WriteLine($"{item.Title} - {item.Category.Name}");
    }
}

static void OneToMany(NpgsqlConnection connection)
{
    var sql = @"SELECT p.id, p.title, t.id as tagid, t.name  FROM post p inner join tag t on 1 = 1";
    var items = connection.Query<PostTags, Tags, PostTags>(
        sql,
        (posttags, tags)=> 
        {
            
            return posttags;
        }, splitOn: "tagid"
    );

    foreach(var item in items)
    {
        Console.WriteLine($"{item.Title}");
        foreach(var tag in item.Tags)
            Console.WriteLine($"Tags :{tag.Name}");
    }
}