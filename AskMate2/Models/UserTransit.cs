using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Models
{
    public class UserTransit
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Reputation { get; set; }
        public DateTime RegistrationDate { get; set; }

        public UserTransit(string id, string email, string password, int reputation)
        {
            Id = id;
            Email = email;
            Password = password;
            Reputation = reputation;
        }

        public UserTransit(string id, string email, string password, int reputation, DateTime registrationDate) : this(id, email, password, reputation)
        {
            RegistrationDate = registrationDate;
        }
    }
}
