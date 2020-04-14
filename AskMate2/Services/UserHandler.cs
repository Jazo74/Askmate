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
        //Missing available users
        private static  DBService dbService = new DBService();
        private List<User> _users;


        // gets all users from the DataBase
        public List<User> GetAll()
        {
            _users = dbService.GetAllUsers();

            return _users;
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




    }
}
