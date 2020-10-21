using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Big_Domashka
{
    public class Person
    {
        public Guid id { get; set; }

        public string role;
        public string login { get; set; }
        public int password { get; set; }
        private string name { get; set; }
        private string surname { get; set; }
        private string phonenumber { get; set; }
        public Person()
        {

        }
        public Person(Manager manager)
        {
            id = manager.id;
            role = manager.role;
            login = manager.login;
            password = manager.password;
            name = manager.name;
            surname = manager.surname;
            phonenumber = manager.phonenumber;
        }
        public Person(Buyer buyer) 
        {
            id = buyer.id;
            role = buyer.role;
            login = buyer.login;
            password = buyer.password;
            name = buyer.name;
            surname = buyer.surname;
            phonenumber = buyer.phonenumber;
        } 
        //private static Person FindUsers_And_Prove(string login_check, int password_check, List<Manager> user)
        //{
        //    var person1 = user.Find(m => m.login == login_check);
        //    if (person1 != null && person1.password.Equals(password_check))
        //    {
        //        Person person = new Person(person1);
        //        return person1;
        //    }
        //    return null;
        //}
        //private static Person FindUsers_And_Prove(string login_check, int password_check, List<Buyer> user)
        //{
        //    var person1 = user.Find(m => m.login == login_check);
        //    if (person1 != null && person1.password.Equals(password_check))
        //    {
        //        Person person = new Person(person1);
        //        return person1;
        //    }
        //    return null;
        //}
        public static Person Sign_in()
        {
            Console.Write("Введите логин  ");
            string login_check = Console.ReadLine();
            Console.Write("Введите пароль  ");
            int password_check = (Console.ReadLine()).GetHashCode();
            var manager = Manager.users_Manager.Find(m => m.login == login_check); //Как тут избавится от дублирования кода
            if (manager != null && manager.password.Equals(password_check)) {
                Person person = new Person(manager);
                return person;
            }
            var buyer =Buyer.users_Buyer.Find(m => m.login == login_check);
            if (buyer != null && buyer.password.Equals(password_check)) {
                Person person = new Person(buyer);
                return person; 
            }
            return null;
        }
    }
}
