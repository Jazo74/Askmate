using AskMate2.Domain;
using AskMate2.Models;
using Newtonsoft.Json.Schema;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Services
{
    public class UserHandler : IUserService
    {
        //Missing available users
        private List<User> _users = new List<User>();

        private List<UserTransit> _users2 = new List<UserTransit>();


        // gets all users from the DataBase

        public List<User> GetAllUsers()
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM \"user\"", conn))
                {
                    List<User> userList = new List<User>();
                    string id = "";
                    string email = "";
                    string password = "";
                    int reputation = 0;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader["user_id"].ToString();
                        email = reader["email"].ToString();
                        password = reader["password"].ToString();
                        reputation = Convert.ToInt32(reader["reputation"]);
                        _users.Add(new User(id, email, password, reputation));
                    }

                    return _users;
                }
            }
        }


        public User GetUserByID(string id)
        {
            return _users.FirstOrDefault(u => u.Id == id);

        }

        public User GetUserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        // 
        public User Login(string email, string password)
        {

            var user = GetUserByEmail(email);
            if (user == null)
            {
                return null;
            }
            if (user.Password != password)
            {
                return null;
            }
            return user;
        }








        //DataBase 

        public void AddUser(string email, string password)
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO user (email, password) VALUES (@email, @password)"))
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void RegisterUser(string user_id, string email, string password)
        {
            DateTime registration_date = DateTime.Now;
            int reputation = 0;

            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("INSERT INTO \"user\"(user_id, email, \"password\", reputation, registration_date) VALUES(@user_id, @email, @password, @reputation, @registration_date)", conn))
                {
                    cmd.Parameters.AddWithValue("@user_id", user_id);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@reputation", reputation);
                    cmd.Parameters.AddWithValue("@registration_date", registration_date);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<UserTransit> GetAllUsersModel()
        {
            DateTime registrationDate = DateTime.Now;
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM \"user\"", conn))
                {
                    string id = "";
                    string email = "";
                    string password = "";
                    int reputation = 0;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader["user_id"].ToString();
                        email = reader["email"].ToString();
                        password = reader["password"].ToString();
                        reputation = Convert.ToInt32(reader["reputation"]);
                        registrationDate = Convert.ToDateTime(reader["registration_date"]);
                        _users2.Add(new UserTransit(id, email, password, reputation, registrationDate));
                    }
                    return _users2;
                }
            }
        }

    }
}
