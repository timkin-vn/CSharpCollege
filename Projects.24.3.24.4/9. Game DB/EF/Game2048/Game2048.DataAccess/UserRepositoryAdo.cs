using System;
using System.Configuration;
using Game2048.Common;
using Game2048.Common.Interfaces;
using Game2048.Common.Models;
using Npgsql;

namespace Game2048.DataAccess
{
    public class UserRepositoryAdo : IUserRepository
    {
        private static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString; }
        }

        public User GetById(int id)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT \"Id\",\"Name\" FROM public.\"Users\" WHERE \"Id\"=@id", conn))
                {
                    cmd.Parameters.AddWithValue("id", id);
                    using (var reader = cmd.ExecuteReader())
                        return reader.Read() ? new User { Id = reader.GetInt32(0), Name = reader.GetString(1) } : null;
                }
            }
        }

        public User GetByName(string name)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT \"Id\",\"Name\" FROM public.\"Users\" WHERE \"Name\"=@name", conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    using (var reader = cmd.ExecuteReader())
                        return reader.Read() ? new User { Id = reader.GetInt32(0), Name = reader.GetString(1) } : null;
                }
            }
        }

        public User Create(string name)
        {
            using (var conn = new NpgsqlConnection(ConnString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "INSERT INTO public.\"Users\" (\"Name\") VALUES (@name) RETURNING \"Id\"", conn))
                {
                    cmd.Parameters.AddWithValue("name", name);
                    int id = Convert.ToInt32(cmd.ExecuteScalar());
                    return new User { Id = id, Name = name };
                }
            }
        }
    }
}
