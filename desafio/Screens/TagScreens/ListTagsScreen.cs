using blog.Models;
using blog.Repositories;
using desafio;

namespace Blog.Screens.TagScreens
{
    public static class ListTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Lista de tags");
            Console.WriteLine("----------");
            List();
            Console.WriteLine();
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        public static void List()
        {
            var repository = new Repository<Tag>(Database.Connection);
            var list = repository.Get();
            foreach(var item in list)
                Console.WriteLine($"{item.Id} - {item.Name} ({item.Slug})");
        }
    }
}