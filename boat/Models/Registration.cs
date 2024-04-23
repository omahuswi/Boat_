using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boat.Models
{
    public class Registration
    {
        User newUser;

        public Registration(User user)
        {
            this.newUser = user;
        }

        public void GetRegistration()
        {
            if (newUser is Customer) 
            {                 
                NpgsqlConnection conn = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand($"select add_new_customer(\'{(newUser as Customer).Login}\', \'{(newUser as Customer).Password}\', \'" +
                    $"{(newUser as Customer).FirstName}\', \'{(newUser as Customer).FamilyName}\', \'{(newUser as Customer).BirthDate:dd.MM.yyyy}\', \'{(newUser as Customer).Email}\', \'" +
                    $"{(newUser as Customer).Phone}\', \'{(newUser as Customer).IdNumber}\', \'{(newUser as Customer).DocumentName}\')");
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            Authorization authorization = new Authorization(newUser);
            authorization.Authorize();
        }
    }
}
