using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace boat.Models
{
    public class Customer: User
    {            
        private string _organisation;
        private string _street;
        private string _house;
        private string _city;

        private string _email;
        private string _phone;
        private string _idNumber;
        private string _documentName;
        private string _firstName;
        private string _familyName;
        private DateTime _birthDate;

        public Customer(string login, string password, int userid) : base(login, password, userid)
        {
            NpgsqlConnection conn = new NpgsqlConnection($"Host=localhost;Port=5432;Username=postgres;Password=11111111;Database=boat");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand($"select * from customer where user_id = {userid}");
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            NpgsqlDataReader dataReader = cmd.ExecuteReader();
            dataReader.Read();

            Email = dataReader["customer_email"].ToString();
            Phone = dataReader["customer_phone"].ToString();
            IdNumber = dataReader["customer_id_number"].ToString();
            DocumentName = dataReader["customer_document_name"].ToString();
            FirstName = dataReader["customer_first_name"].ToString();
            FamilyName = dataReader["customer_family_name"].ToString();
            BirthDate = Convert.ToDateTime(dataReader["customer_date_of_birth"]);
            Organisation = dataReader["customer_organisation"].ToString();
            Street = dataReader["customer_street"].ToString();
            House = dataReader["customer_house"].ToString() ;
            City = dataReader["customer_city"].ToString();
            conn.Close();
        }
        

        public Customer(string login, string password, string email, string phone, string idNumber, string documentName, string firstName, string familyName, DateTime birthDate) : base(login, password)
        {
            Email = email;
            Phone = phone;
            IdNumber = idNumber;
            DocumentName = documentName;
            FirstName = firstName;
            FamilyName = familyName;
            BirthDate = birthDate;
        }

        public string Organisation { get => _organisation; set => _organisation = value; }
        public string Street { get => _street; set => _street = value; }
        public string House { get => _house; set => _house = value; }
        public string City { get => _city; set => _city = value; }
        public string Email { get => _email; set => _email = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public string IdNumber { get => _idNumber; set => _idNumber = value; }
        public string DocumentName { get => _documentName; set => _documentName = value; }
        public string FirstName { get => _firstName; set => _firstName = value; }
        public string FamilyName { get => _familyName; set => _familyName = value; }
        public DateTime BirthDate { get => _birthDate; set => _birthDate = value; }
    }
}
