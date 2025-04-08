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
    public class CommentRepository : ICommentRepository
    {
        private readonly string _connectionString;

        public CommentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Comment comment)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO Comments (Content, UserId, IssueId) VALUES (@Content, @UserId, @IssueId) RETURNING Id";
                comment.Id = connection.ExecuteScalar<int>(sql, comment);
            }
        }

        public Comment GetById(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Comments WHERE Id = @Id";
                return connection.QuerySingleOrDefault<Comment>(sql, new { Id = id });
            }
        }

        public IEnumerable<Comment> GetByIssueId(int issueId)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                var sql = "SELECT * FROM Comments WHERE IssueId = @IssueId";
                return connection.Query<Comment>(sql, new { IssueId = issueId });
            }
        }
    }
}