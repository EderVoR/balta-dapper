using Blog.Models;
using Dapper.Contrib.Extensions;
using Npgsql;

namespace Blog.Repositories
{
    public class UserRepository
    {

        private NpgsqlConnection _connection = new NpgsqlConnection();

        public IEnumerable<Usuario> Get()
            => _connection.GetAll<Usuario>();        

        public Usuario Get(int id)
            => _connection.Get<Usuario>(id);

        public void Create(Usuario user)
            => _connection.Insert<Usuario>(user);
    }
}
