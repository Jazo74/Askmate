using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AskMate2.Domain
{
    public interface IUserService
    {

        public List<User> GetAll();

        public User GetUserByID(string id);

        public User GetUserByEmail(string email);

        public User Login(string email, string password);

        void AddUser(string email, string password);



    }
}
