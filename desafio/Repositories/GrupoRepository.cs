using Blog.Models;
using Dapper.Contrib.Extensions;
using Npgsql;

namespace Blog.Repositories
{
    public class GrupoRepository
    {
        private readonly NpgsqlConnection _connection;

        public GrupoRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Grupo> Get()
            => _connection.GetAll<Grupo>();        

        public Grupo Get(int id)
            => _connection.Get<Grupo>(id);

        public void Create(Grupo user)
            => _connection.Insert<Grupo>(user);
    }
}