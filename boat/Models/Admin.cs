using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boat.Models
{
    internal class Admin : User
    {
        public Admin(string login, string password, int id) : base(login, password, id)
        {
        }
    }
}
