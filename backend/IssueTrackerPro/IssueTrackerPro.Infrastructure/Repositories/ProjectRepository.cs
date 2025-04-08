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
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString;

        public ProjectRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Project project)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Projects (Name, Description) VALUES (@Name, @Description) RETURNING Id";
                project.Id = connection.ExecuteScalar<int>(sql, project);
            }
        }

        public Project GetById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Projects WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Project>(sql, new { Id = id });
            }
        }

        public IEnumerable<Project> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Projects";
                return connection.Query<Project>(sql);
            }
        }

        public void Update(Project project)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE Projects SET Name = @Name, Description = @Description WHERE Id = @Id";
                connection.Execute(sql, project);
            }
        }
    }
}