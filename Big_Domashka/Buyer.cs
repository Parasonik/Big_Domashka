using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;

namespace Big_Domashka
{
    public class Buyer : Person
    {
        public static List<Buyer> users_Buyer = new List<Buyer>();
        public Guid id { get; set; }

        public string role = "buyer";
        public string login { get; set; }
        public int password { get; set; }
        private string name { get; set; }
        private string surname { get; set; }
        public string phonenumber { get; set; }
        private string address { get; set; }
        private DateTime dateOfbirth{ get; set; }
        private DateTime dateOfRegister { get; set; }
        private Guid shoppingCartId { get; set; }
        public List<Order> listOfOrders = new List<Order>();  
        public static Buyer Registration() 
        {
            Buyer Ghost = new Buyer();
            Ghost.id = Guid.NewGuid();
            Ghost.shoppingCartId = Guid.NewGuid();
            Console.WriteLine("Введите логин");
            Ghost.login = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            Ghost.password = (Console.ReadLine()).GetHashCode();
            Console.WriteLine("Введите своё имя");
            Ghost.name = Console.ReadLine();
            Console.WriteLine("Введите свою фамилию");
            Ghost.surname = Console.ReadLine();
            Console.WriteLine("Введите свой номер телефона");
            Ghost.phonenumber = Console.ReadLine();
            Console.WriteLine("Введите свой адрес");
            Ghost.address = Console.ReadLine();
            Console.WriteLine("Введите дату своего рождения");
            Ghost.dateOfbirth = DateTime.Parse(Console.ReadLine());
            Ghost.dateOfRegister = DateTime.Today;
            Ghost.listOfOrders = null;
            return Ghost;
        }
    }
}
