using Dapper.Contrib.Extensions;
using Npgsql;

namespace blog.Repositories
{
    public class Repository<T> where T : class
    {
        public readonly NpgsqlConnection _connection;

        public Repository(NpgsqlConnection connection)
            => _connection = connection; 

        public IEnumerable<T> Get()
            => _connection.GetAll<T>();

        public void Create()
            => _connection.CreateCommand();
    }
}