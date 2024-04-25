using Npgsql;
using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.Data;
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
        private DateTime _userLastVisit;        

        public User(string login, string password)
        {            
            Login = login;
            Password = password;                       
        }
        public User(string login, string password, int id)
        {
            Id = id;
            Login = login;
            Password = password;
            UserLastVisit = DateTime.Now;            
        }

        public int Id { get => _userId; set => _userId = value; }
        public string Login { get => _userLogin; set => _userLogin = value; }
        public string Password { get => _userPassword; set => _userPassword = value; }
        public DateTime UserLastVisit { get => _userLastVisit; set => _userLastVisit = value; }

        public void UpdateLastVisit()
        {
            NpgsqlConnection npgsqlConnection = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            if (npgsqlConnection.State == ConnectionState.Open)
            {
                npgsqlConnection.Close();
            }
            npgsqlConnection.Open();
            NpgsqlCommand npgsqlCommand = new NpgsqlCommand($"select update_last_visit ({Id})");
            npgsqlCommand.Connection = npgsqlConnection;
            npgsqlCommand.CommandType = CommandType.Text;
            npgsqlCommand.ExecuteNonQuery();
            npgsqlConnection.Close();
        }
    }

    
}
