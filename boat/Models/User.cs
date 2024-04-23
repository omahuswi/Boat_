using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boat.Models
{
    public class User
    {
        private int _userId;
        private string _userLogin;
        private string _userPassword;
        

        public User(string login, string password)
        {            
            Login = login;
            Password = password;            
        }

        public int Id { get => _userId; set => _userId = value; }
        public string Login { get => _userLogin; set => _userLogin = value; }
        public string Password { get => _userPassword; set => _userPassword = value; }


        public void GetUserInfoFromDB()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            npgsqlConnection.Open();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand(); //TODO функция поиска информации по человеку 
        }
        
    }
}
