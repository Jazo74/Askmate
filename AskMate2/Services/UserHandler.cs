using AskMate2.Domain;
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

        private List<User> _users = new List<User>();


        // gets all users from the DataBase
        public List<User> GetAll()
        {
            using (var conn = new NpgsqlConnection(Program.ConnectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM user"))
                {
                    List<User> userList = new List<User>();
                    var id = 0;
                    string email = "";
                    string password = "";

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader["user_id"]);
                        email = reader["email"].ToString();
                        password = reader["password"].ToString();
                    }
                    _users.Add(new User(id, email, password));
                    return _users;
                }
            }
        }

        public User GetOne(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);

        }

        public User GetOne(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        // 
        public User Login(string email, string password)
        {

            var user = GetOne(email);
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




    }
}
