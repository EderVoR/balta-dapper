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
    //OneToMany(connection);
    //QueryMutiple(connection);
    //SelectIn(connection);
    //SelectLike(connection);
    Transaction(connection);
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
    var tagsItem = new List<PostTags>();
    var items = connection.Query<PostTags, Tags, PostTags>(
        sql,
        (posttags, tags)=> 
        {
            var tag = tagsItem.Where(x => x.Id == posttags.Id).FirstOrDefault();
            if(tag == null)
            {
                tag = posttags;
                tag.Tags.Add(tags);
                tagsItem.Add(tag);
            }
            else
            {
                tag.Tags.Add(tags);
            }
            return posttags;
        }, splitOn: "tagid"
    );

    foreach(var item in tagsItem)
    {
        Console.WriteLine($" -> {item.Title}");
        foreach(var tag in item.Tags)
            Console.WriteLine($"Tags :{tag.Name}");
    }
}

static void QueryMutiple(NpgsqlConnection connection)
{
    var sql = "select * from category; select * from tag;";

    using(var multi = connection.QueryMultiple(sql))
    {
        var categories = multi.Read<Category>();
        var tags = multi.Read<Tags>();

        foreach(var item in categories)
            Console.WriteLine($"categoria: {item.Name}");

        foreach(var item in tags)
            Console.WriteLine($"tags: {item.Name}");
    }
}

static void SelectIn(NpgsqlConnection connection)
{
    var sql = "select * from tag where name = any (@name)";

    var result = connection.Query<Tags>(sql, new 
    {
        name = new[]
        {
            "C#",
            "Dapper"
        }
    });

    foreach(var item in result)
        Console.WriteLine($"result: {item.Name}");
}

static void SelectLike(NpgsqlConnection connection)
{
    var sql = "select * from category where name like @filtro";

    var result = connection.Query<Tags>(sql, new 
    {
        filtro = "%bac%"
    });

    foreach(var item in result)
        Console.WriteLine($"result: {item.Name}");
}

static void Transaction(NpgsqlConnection connection)
{
     var newCategory = new Category
    {
        Name = "Novo curso2",
        Slug = "novo-curso2"
    };

    var insertDado = @"insert into category (name, slug) 
                    VALUES (@Name, @Slug)";

    connection.Open();
    using(var transaction = connection.BeginTransaction())
    {
        var rows = connection.Execute(insertDado, new
        {
            Name = newCategory.Name,
            Slug = newCategory.Slug
        }, transaction);

        transaction.Commit();
        //transaction.Rollback();

        Console.WriteLine($"{rows} linhas afetadas");
    }
}