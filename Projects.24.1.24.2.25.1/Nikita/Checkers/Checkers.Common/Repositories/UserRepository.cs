using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers.Business.Models;
using Checkers.Common.Contracts;
using Dapper;
using Npgsql;

namespace Checkers.Common.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly IUserApiClient _userApiClient;

        public UserRepository(string connectionString, IUserApiClient userApiClient)
        {
            _connectionString = connectionString;
            _userApiClient = userApiClient;
        }

        public async Task<UserSession> GetUserId(int userId)
        {
            const string sql = "Select * from users where id = @userId";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var user = await connection.QueryFirstOrDefaultAsync<UserSession>(sql, new
                {
                    userId
                });
                return user;
            }
        }

        public async Task<bool> RegisterUserAsync(string username)
        {
            const string sql = @"Insert into users (id, username) values (1, @username)";
            using(var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var rowAffected = await connection.ExecuteAsync(sql, new {
                    username
                });
                return rowAffected > 0;
            }
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            const string sql = "select count(*) from users where username = @username";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var count = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    username
                });
                return count > 0;
            }
        }

        public async Task<bool> ValidateUserAsync(string username)
        {
            const string sql = @"select count(*) from users where username = @username";
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                var count = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    username
                });
                return count > 0;
            }
        }
    }
}
