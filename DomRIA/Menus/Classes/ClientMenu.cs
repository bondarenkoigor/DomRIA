using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DomRIA.DescriptionInfo;
using DomRIA.Users;
using DomRIA.Menus.Interfaces;

namespace DomRIA.Menus.Classes
{
    internal class ClientMenu : IMenu
    {
        public Client _Client { get; set; }
        public ClientMenu() => _Client = new Client();

        public void Start()
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Console.Write("1 - Все дома\n2 - По возрастанию цены\n3 - по убыванию цены\n4 - по городу\n5 - по владельцу\n6 - написать владельцу\n0 - выход\n\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        PrintProducts(_Client.Products);
                        break;
                    case 2:
                        {
                            var sorted = _Client.Products.OrderBy(product => product.Price.UAH).ToList<Product>();
                            PrintProducts(sorted);
                            break;
                        }
                    case 3:
                        {
                            var sorted = _Client.Products.OrderByDescending(product => product.Price.UAH).ToList<Product>();
                            PrintProducts(sorted);
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Введите город:");
                            string city = Console.ReadLine();
                            List<Product> sorted = _Client.Products.TakeWhile<Product>((product) => product.Geo.City.ToLower() == city.ToLower()).ToList();
                            PrintProducts(sorted);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Введите имя владельца:");
                            string name = Console.ReadLine();
                            List<Product> sorted = _Client.Products.TakeWhile<Product>((product) => product.Owner.Name.ToLower() == name.ToLower()).ToList();
                            PrintProducts(sorted);
                            break;
                        }
                    case 6:
                        {
                            _Client.ShowAll();
                            Console.Write("Владельцу какого дома вы хотите написать:");
                            int ind = int.Parse(Console.ReadLine());
                            string ownerName = _Client.Products[ind - 1].Owner.Name;
                            Console.Write("Ваш номер телефона:");
                            string number = Console.ReadLine();
                            Console.Write("Сообщение:");
                            string message = Console.ReadLine();
                            if (!Directory.Exists("messages")) Directory.CreateDirectory($"messages");
                            File.AppendAllText($@"messages\{ownerName}.txt", $"[{number}] {message}");

                            break;
                        }
                }

            } while (choice != 0);
        }
        public void PrintProducts(List<Product> products)
        {
            for (int i = 0; i < products.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{i + 1}. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(products[i].Show());
            }
            Console.Write("Нажмите Enter, чтобы продолжить...");
            Console.ReadLine();
        }
    }
}
