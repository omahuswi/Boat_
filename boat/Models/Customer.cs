using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
