using System.Collections.Generic;
using Dapper;
using IssueTrackerPro.Domain.Entities;
using IssueTrackerPro.Domain.Interfaces.Repositories;
using Npgsql;

namespace IssueTrackerPro.Infrastructure.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly string _connectionString;

        public IssueRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Issue issue)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Issues (Title, Description, Status, Priority, AssignedUserId, ProjectId, CreatedByUserId) " +
                          "VALUES (@Title, @Description, @Status, @Priority, @AssignedUserId, @ProjectId, @CreatedByUserId) RETURNING Id";
                return connection.ExecuteScalar<int>(sql, issue);
            }
        }

        public Issue GetById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Issues WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Issue>(sql, new { Id = id });
            }
        }

        public IEnumerable<Issue> GetByProjectId(int projectId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Issues WHERE ProjectId = @ProjectId";
                return connection.Query<Issue>(sql, new { ProjectId = projectId });
            }
        }

        public IEnumerable<Issue> GetByUserId(int userId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                // AssignedUserId yerine CreatedByUserId ile filtreleme yapıyoruz
                var sql = "SELECT * FROM Issues WHERE CreatedByUserId = @UserId";
                return connection.Query<Issue>(sql, new { UserId = userId });
            }
        }

        public void Update(Issue issue)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE Issues SET Title = @Title, Description = @Description, Status = @Status, " +
                          "Priority = @Priority, AssignedUserId = @AssignedUserId, ProjectId = @ProjectId " +
                          "WHERE Id = @Id";
                connection.Execute(sql, issue);
            }
        }
        public IEnumerable<Issue> GetAll()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Issues";
                return connection.Query<Issue>(sql);
            }
        }
    }
}