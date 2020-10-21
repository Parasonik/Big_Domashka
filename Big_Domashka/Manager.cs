using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
namespace Big_Domashka
{
    public class Manager : Person
    {
        public static List<Manager> users_Manager = new List<Manager>();
        public Guid id { get; set; }

        public string role = "manager";
        public  string login { get; set; }
        public int password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phonenumber { get; set; }

        public Manager(string login1, int password1, string name1, string surname1, string phonenumber1)
        {
            id = Guid.NewGuid();
            role = "manager";
            login = login1;
            password = password1;
            name = name1;
            surname = surname1;
            phonenumber = phonenumber1;
        }
    }
}
