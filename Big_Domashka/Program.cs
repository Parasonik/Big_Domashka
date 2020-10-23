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
            Manager Alexandr = new Manager("admin", ("admin").GetHashCode(), "Ivan", "Ivanov", "+375555555555");
            Manager.users_Manager.Add(Alexandr);
            mikpres.listOfProducts.Add(Product.Add("laptop", 55, 5, "нормас, мне нравится")); 
            mikpres.listOfProducts.Add(Product.Add("mouse", 4, 20, "нормас, мне нравится"));
            mikpres.listOfProducts.Add(Product.Add("keyboard", 6, 15, "нормас, мне нравится"));
            mikpres.listOfProducts.Add(Product.Add("carpet", 2, 100, "нормас, мне нравится"));
            mikpres.listOfProducts.Add(Product.Add("cup", 1, 100, "нормас, мне нравится"));
            Console.WriteLine("////////////////////////////////////");
            Console.WriteLine("Приветсвуем в нашем магазине МиКПРЭС");
            Console.WriteLine("////////////////////////////////////");
            bool exit_From_Shop = true;
            bool exit_From_Buyer = true;
            bool exit_From_Manager = true;
            while (exit_From_Shop)
            {
                Console.WriteLine("Введите r если хотите зарегистрироваться, Введите s если хотите войти");
                switch (Console.ReadLine())
                {
                    case "r":
                        Buyer.users_Buyer.Add(Buyer.Registration());
                        break;
                    case "s":
                        Check_Role();
                        break;
                    default:
                        exit_From_Shop = false;
                        break;
                }
            }
            void Check_Role()
            {
                var foundedUser = Person.Sign_in();
                if (foundedUser != null)
                {
                    if (foundedUser.role == "manager")
                    {
                        Manager_Moves();
                    }
                    else
                    {
                        Buyer_Moves(foundedUser);
                    }
                }
                else
                {
                    Console.WriteLine("Неправильный логин или пароль");
                }
            }
            void Output_Data_Products()
            {
                Console.WriteLine("Введите имя продукта о котором вы хоитие просмотреть информацию");
                Product product= mikpres.listOfProducts.Find(m => m.name == Console.ReadLine());
                Console.WriteLine(product.name);
                Console.WriteLine(product.description);
                Console.WriteLine(product.price);
                Console.WriteLine(product.amount);
            }
            void Output_Name_Products()
            {
                for (int i = 0; i < mikpres.listOfProducts.Count; i++)
                {
                    Console.WriteLine(mikpres.listOfProducts[i].name);
                }
            }
            void Menu_Manager()
            {
                Console.WriteLine("Введите 1 если хотите выйти из аккаунта");
                Console.WriteLine("Введите 2 если хотите добавить товар в список товаров");
                Console.WriteLine("Введите 3 если хотите удалить товар из списка");
                Console.WriteLine("Введите 4 если хотите увидеть список товаров");
                Console.WriteLine("Введите 5 если хотите подробную информацию о товарах");
            }
            void Menu_Buyer()
            {
                Console.WriteLine("Введите 1 если хотите выйти из аккаунта");
                Console.WriteLine("Введите 2 если хотите просмотреть список товаров");
                Console.WriteLine("Введите 3 если хотите просмотреть подробную информацию о товаре");
                Console.WriteLine("Введите 4 если хотите добавить товар в корзину");
                Console.WriteLine("Введите 5 если хотите удалить товар из корзины");
                Console.WriteLine("Введите 6 если хотите очистить корзину");
                Console.WriteLine("Введите 7 если хотите изменить кол-во товаров в корзине");
                Console.WriteLine("Введите 8 если хотите оформить заказ");
                Console.WriteLine("Введите 9 если хотите просмотреть свои заказы");
                Console.WriteLine("Введите 10 если хотите просмотреть подробности о своём заказе");
            }
            void Manager_Moves()
            {
                while (exit_From_Manager)
                {
                    Menu_Manager();
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
            void Product_Buy(ShoppingCart shoppingCart)
            {
                Console.WriteLine("Введите имя товара который бы вы хотели купить");
                string name = Console.ReadLine();
                Console.WriteLine("Введите в каком количесвте вы хотите купить товар");
                double enter_amount = Convert.ToInt32(Console.ReadLine());
                Guid id = mikpres.listOfProducts.Find(m => m.name == name).id;
                shoppingCart.listOfOrderItems.Add(OrderItem.Get(id, enter_amount));
                int product_index = mikpres.listOfProducts.FindIndex(m => m.name == name);
                mikpres.listOfProducts[product_index].amount -= enter_amount;
            }
            void Delete_Order_Item(ShoppingCart shoppingCart)
            {
                Console.WriteLine("Введите имя товара который бы вы хотели удалить");
                string name = Console.ReadLine();
                Guid productId = mikpres.listOfProducts.Find(m => m.name == name).id;
                mikpres.listOfProducts[mikpres.listOfProducts.FindIndex(m => m.name == name)].amount += shoppingCart.listOfOrderItems.Find(m => m.productId == productId).count;
                shoppingCart.listOfOrderItems.RemoveAt(shoppingCart.listOfOrderItems.FindIndex(m => m.productId == productId));
            }
            void Change_Order_Item(ShoppingCart shoppingCart)
            {
                Console.WriteLine("Введите имя товара который бы вы хотели отредактировать");
                string name = Console.ReadLine();
                Console.WriteLine("Введите какое количество товара вы бы хотели удалить");
                double enter_amount = Convert.ToInt32(Console.ReadLine());
                Guid product_id = mikpres.listOfProducts.Find(m => m.name == name).id;
                shoppingCart.listOfOrderItems.Find(m => m.productId == product_id).count -= enter_amount;
                int product_index = mikpres.listOfProducts.FindIndex(m => m.name == name);
                mikpres.listOfProducts[product_index].amount += enter_amount;
            }
            void Make_Order(int index, ShoppingCart shoppingCart)
            {
                if (shoppingCart.listOfOrderItems == null)
                {
                    Console.WriteLine("В корзине нет заказов");
                }
                else
                {
                    Order order = new Order(Buyer.users_Buyer[index].id, shoppingCart.listOfOrderItems);
                    var i = Buyer.users_Buyer.FindIndex(m => m.login == Buyer.users_Buyer[index].login);
                    if (i >= 0)
                    {
                        if (Buyer.users_Buyer[i].listOfOrders==null)
                        {
                            Buyer.users_Buyer[i].listOfOrders = new List<Order>();
                        }
                        Buyer.users_Buyer[i].listOfOrders.Add(order); // Тут ошибка не могу понять в чём
                    }
                    else
                    {
                        Console.WriteLine("User not found");
                    }
                }
            }
            void Check_ListOfOrder(int index)
            {
                if (Buyer.users_Buyer[index].listOfOrders != null)
                {
                    Guid product_id;
                    for (int i = 0; i < Buyer.users_Buyer[index].listOfOrders.Count; i++)
                    {
                        for (int j = 0; j < Buyer.users_Buyer[index].listOfOrders[i].listOfOrderItems.Count; j++)
                        {
                            product_id = Buyer.users_Buyer[index].listOfOrders[i].listOfOrderItems[j].productId;
                            Console.WriteLine(mikpres.listOfProducts.Find(m => m.id == product_id).name);
                            Console.WriteLine(Buyer.users_Buyer[index].listOfOrders[i].listOfOrderItems[j].count);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Список заказов пуст");
                }
            }
            void Check_Order_Data(int index)
            {
                Console.WriteLine("Введите имя заказа о котором вы бы хотели узнать подробную информацию");
                Guid productId = mikpres.listOfProducts.Find(m => m.name == Console.ReadLine()).id;
                Console.WriteLine(Buyer.users_Buyer[index].listOfOrders.Find(m => m.listOfOrderItems.Find(k => k.productId == productId).productId == productId).dateOfOrder);
            }
            void Buyer_Moves(Person foundedUser)
            {
                ShoppingCart shoppingCart = new ShoppingCart();
                int index_Of_User = Buyer.users_Buyer.FindIndex(m => m.login == foundedUser.login);
                Console.WriteLine(index_Of_User);
                while (exit_From_Buyer)
                {
                    Menu_Buyer();
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
                            Product_Buy(shoppingCart);
                            break;
                        case "5":
                            Delete_Order_Item(shoppingCart);
                            break;
                        case "6":
                            shoppingCart.listOfOrderItems.Clear();
                            break;
                        case "7":
                            Change_Order_Item(shoppingCart);
                            break;
                        case "8":
                            Make_Order(index_Of_User, shoppingCart);
                            break;
                        case "9":
                            Check_ListOfOrder(index_Of_User);
                            break;
                        case "10":
                            Check_Order_Data(index_Of_User);
                            break;
                    }
                }
            }
        }
    }
}
