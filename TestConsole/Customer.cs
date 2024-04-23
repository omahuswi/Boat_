using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Customer : User
    {
        public Customer(int id, string login, string password, string name, string address, string city) : base (id, login, password)
        {
            Name = name;
            Address = address;
            City = city;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }



    }
}
