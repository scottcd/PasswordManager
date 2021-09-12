using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PasswordManager.Models {
    public class Credentials {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Account { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials() { 
        
        }

        public Credentials(string account, string email, string username, string password) {
            Account = account;
            Email = email;
            Username = username;
            Password = password;
        }

    }
}
