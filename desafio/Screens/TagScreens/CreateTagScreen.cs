using blog.Models;
using blog.Repositories;
using desafio;

namespace Blog.Screens.TagScreens
{
    public static class CreateTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Nova tag");
            Console.WriteLine("----------");

            Console.WriteLine("Nome: ");
            var name = Console.ReadLine();

            Console.WriteLine("Slug: ");
            var slug = Console.ReadLine();

            var tag = new Tag
            {
                Name = name,
                Slug = slug
            };

            Create(tag);
            Console.WriteLine();
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        public static void Create(Tag tag)
        {
            var repository = new Repository<Tag>(Database.Connection);
            repository.Create();
        }
    }
}