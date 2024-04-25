using Npgsql;
using Npgsql.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boat.Models
{
    public class DBStatement
    {
        private static readonly DBStatement instance = new DBStatement();
        private readonly NpgsqlConnection conn;

        private DBStatement()
        {
            conn = new NpgsqlConnection("Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
        }

        public static DBStatement Instance
        {
            get
            {
                return instance;
            }
        }

        public NpgsqlConnection GetConnection()
        {
            return conn;
        }
    }
}
