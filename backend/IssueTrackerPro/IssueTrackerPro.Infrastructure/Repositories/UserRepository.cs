using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using Npgsql;

namespace IssueTrackerPro.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Users (Username, PasswordHash, Role) VALUES (@Username, @PasswordHash, @Role) RETURNING Id";
                user.Id = connection.ExecuteScalar<int>(sql, user);
            }
        }

        public User GetById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users WHERE Id = @Id";
                return connection.QuerySingleOrDefault<User>(sql, new { Id = id });
            }
        }

        public User GetByUsername(string username)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users WHERE Username = @Username";
                return connection.QuerySingleOrDefault<User>(sql, new { Username = username });
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Users";
                return connection.Query<User>(sql);
            }
        }

        public void Update(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash WHERE Id = @Id";
                connection.Execute(sql, user);
            }
        }
    }
}