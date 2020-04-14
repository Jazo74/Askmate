using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public class User
    {

        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Reputation { get; set; }

        public User(string id, string email, string password, int reputation)
        {
            Id = id;
            Email = email;
            Password = password;
            Reputation = reputation;
        }
    }
}
