using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new Customer(1, "ddjj", "kdkd", "jdjd", "kdkdkd", "dkdkd");

            Console.WriteLine(user.Login);
        }
    }
}
