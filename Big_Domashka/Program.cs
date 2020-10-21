using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace Big_Domashka
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Shop mikpres = Shop.getInstance();
            Manager Alexandr = new Manager();
            Alexandr.id = Guid.NewGuid();
            Alexandr.login = "admin";
            Alexandr.password = ("admin").GetHashCode();
            Alexandr.name = "Ivan";
            Alexandr.surname = "Ivanov";
            Alexandr.phonenumber = "+375555555555";
            Person.users_Manager.Add(Alexandr);
            mikpres.listOfProducts.Add(Product.Add("laptop", 55, 5, "нормас, мне нравится")); 
            mikpres.listOfProducts.Add(Product.Add("mouse", 4, 20, "нормас, мне нравится"));
            mikpres.listOfProducts.Add(Product.Add("keyboard", 6, 15, "нормас, мне нравится"));
            mikpres.listOfProducts.Add(Product.Add("carpet", 2, 100, "нормас, мне нравится"));
            mikpres.listOfProducts.Add(Product.Add("cup", 1, 100, "нормас, мне нравится"));
            Console.WriteLine("////////////////////////////////////");
            Console.WriteLine("Приветсвуем в нашем магазине МиКПРЭС");
            Console.WriteLine("////////////////////////////////////");
            bool b = true;
            bool exit_From_Buyer = true;
            bool exit_From_Manager = true;
            while (b) // если сделать loop через рекурсию будет лучше или нет?
            {
                Console.WriteLine("Введите r если хотите зарегистрироваться, Введите s если хотите войти");
                switch (Console.ReadLine())
                {
                    case "r":
                        Buyer ghost = new Buyer();
                        Person.users_Buyer.Add(ghost.Registration());
                        break;
                    case "s":
                        var foundedUser = Person.Sign_in();
                        if (foundedUser != null)
                        {

                            if (foundedUser.role == "manager")
                            {
                                Manager_Moves();
                            }
                            else
                            {
                                Buyer_Moves();
                            }

                        }
                        else
                        {
                            Console.WriteLine("Неправильный логин или пароль");
                        }
                        break;
                    default:
                        b = false;
                        break;
                }
            }
            void Output_Data_Products()
            {
                for (int i = 0; i < mikpres.listOfProducts.Count; i++)
                {
                    Console.WriteLine(mikpres.listOfProducts.ElementAt(i).name);
                    Console.WriteLine(mikpres.listOfProducts.ElementAt(i).description);
                    Console.WriteLine(mikpres.listOfProducts.ElementAt(i).price);
                    Console.WriteLine(mikpres.listOfProducts.ElementAt(i).amount);
                }
            }
            void Output_Name_Products()
            {
                for (int i = 0; i < mikpres.listOfProducts.Count; i++)
                {
                    Console.WriteLine(mikpres.listOfProducts.ElementAt(i).name);
                }
            }
            void Manager_Moves()
            {
                while (exit_From_Manager)
                {
                    Console.WriteLine("Введите 1 если хотите выйти из аккаунта");
                    Console.WriteLine("Введите 2 если хотите добавить товар в списко товаров");
                    Console.WriteLine("Введите 3 если хотите удалить товар из списка");
                    Console.WriteLine("Введите 4 если хотите увидеть список товаров");
                    Console.WriteLine("Введите 5 если хотите подробную информацию о товарах");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            exit_From_Manager = false;
                            break;
                        case "2":
                            mikpres.listOfProducts.Add(Product.Add());
                            break;
                        case "3":
                            Console.WriteLine("Введите имя продукта который вы хотите удалить");
                            Product.Delete(Console.ReadLine(), ref mikpres);
                            break;
                        case "4":
                            Output_Name_Products();
                            break;
                        case "5":
                            Output_Data_Products();
                            break;


                    }
                }
            }
            void Buyer_Moves()
            {
                ShoppingCart shoppingCart = new ShoppingCart();
                Order order = new Order();
                while (exit_From_Buyer)
                {
                    Console.WriteLine("Введите 1 если хотите выйти из аккаунта");
                    Console.WriteLine("Введите 2 если хотите просмотреть список товаров");
                    Console.WriteLine("Введите 3 если хотите просмотреть подробную информацию о товаре");
                    Console.WriteLine("Введите 4 если хотите добавить товар в корзину");
                    Console.WriteLine("Введите 5 если хотите удалить товар из корзины");
                    Console.WriteLine("Введите 6 если хотите очистить корзину");
                    Console.WriteLine("Введите 7 если хотите изменить кол-во товаров в корзине");
                    Console.WriteLine("Введите 8 если хотите оформить заказ");
                    Console.WriteLine("Введите 9 если хотите посмотреть подробности о своём заказе");
                    switch (Console.ReadLine())
                    {
                        case "1":
                            exit_From_Buyer = false;
                            break;
                        case "2":
                            Output_Name_Products();
                            break;
                        case "3":
                            Output_Data_Products();
                            break;
                        case "4":
                            Console.WriteLine("Введите имя товара который бы вы хотели купить");
                            string name = Console.ReadLine();
                            Console.WriteLine("Введите в каком количесвте вы хотите купить товар");
                            double enter_amount = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < mikpres.listOfProducts.Count; i++)
                            {
                                if (name == mikpres.listOfProducts.ElementAt<Product>(i).name)
                                {
                                    shoppingCart.listOfOrderItems.Add(mikpres.listOfProducts.ElementAt<Product>(i));
                                    shoppingCart.listOfOrderItems.ElementAt<Product>(shoppingCart.listOfOrderItems.Count - 1).amount = enter_amount;
                                    mikpres.listOfProducts.ElementAt<Product>(i).amount -= enter_amount;
                                    break;
                                }

                            }
                            break;
                        case "5":
                            Console.WriteLine("Введите имя товара который бы вы хотели удалить");
                            string name1 = Console.ReadLine();
                            for (int i = 0; i < shoppingCart.listOfOrderItems.Count; i++)
                            {
                                if (name1 == shoppingCart.listOfOrderItems.ElementAt<Product>(i).name)
                                {
                                    for (int j = 0; j < mikpres.listOfProducts.Count; j++)
                                    {
                                        if (name1 == mikpres.listOfProducts.ElementAt<Product>(j).name)
                                            mikpres.listOfProducts.ElementAt<Product>(j).amount += shoppingCart.listOfOrderItems.ElementAt<Product>(i).amount;
                                        break;
                                    }
                                    shoppingCart.listOfOrderItems.RemoveAt(i);
                                    break;
                                }

                            }
                            break;
                        case "6":
                            for (int i = 0; i < shoppingCart.listOfOrderItems.Count; i++)
                            {
                                shoppingCart.listOfOrderItems.RemoveAt(i);
                            }
                            break;
                        case "7":
                            Console.WriteLine("Введите имя товара который бы вы хотели отредактировать");
                            string name2 = Console.ReadLine();
                            Console.WriteLine("Введите какое количество товара вы бы хотели удалить");
                            double enter_amount1 = Convert.ToInt32(Console.ReadLine());
                            for (int i = 0; i < shoppingCart.listOfOrderItems.Count; i++)
                            {
                                if (name2 == shoppingCart.listOfOrderItems.ElementAt<Product>(i).name)
                                {
                                    shoppingCart.listOfOrderItems.ElementAt<Product>(i).amount -= enter_amount1;
                                    for (int j = 0; j < mikpres.listOfProducts.Count; j++)
                                    {
                                        if (name2 == mikpres.listOfProducts.ElementAt<Product>(j).name)
                                            mikpres.listOfProducts.ElementAt<Product>(j).amount += enter_amount1;
                                    }
                                    break;
                                }

                            }
                            break;
                        case "8":
                            if (shoppingCart.listOfOrderItems == null)
                            {
                                Console.WriteLine("В корзине нет заказов");
                            }
                            else
                            {
                                for (int i = 0; i < shoppingCart.listOfOrderItems.Count; i++)
                                {
                                    order.listOfOrderItems.Add(OrderItem.Add(shoppingCart.listOfOrderItems.ElementAt<Product>(i).id, shoppingCart.listOfOrderItems.ElementAt<Product>(i).amount));
                                }
                            }
                            break;
                        case "9":
                            if (order.listOfOrderItems != null)
                            {
                                for (int i = 0; i < order.listOfOrderItems.Count; i++)
                                {
                                    for (int j = 0; j < mikpres.listOfProducts.Count; j++)
                                    {
                                        if (order.listOfOrderItems.ElementAt<OrderItem>(i).productId == mikpres.listOfProducts.ElementAt<Product>(j).id)
                                        {
                                            Console.WriteLine(mikpres.listOfProducts.ElementAt<Product>(j).name);
                                            Console.WriteLine(mikpres.listOfProducts.ElementAt<Product>(j).description);
                                            Console.WriteLine(mikpres.listOfProducts.ElementAt<Product>(j).price);
                                            Console.WriteLine(order.listOfOrderItems.ElementAt<OrderItem>(i).count);

                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Список заказов пуст");
                            }
                            break;



                    }
                }
            }
        }
        
    }
}
