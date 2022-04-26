using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using DomRIA.DescriptionInfo;
using DomRIA.Users;
using DomRIA.Menus.Interfaces;


namespace DomRIA.Menus.Classes
{
    internal class ManagerMenu : IMenu
    {
        public List<Manager> Managers { get; set; }
        public ManagerMenu()
        {
            Managers = new List<Manager>();
            if (!Directory.Exists("Houses")) return;
            foreach (string filePath in Directory.EnumerateFiles("Houses", "*.txt"))
            {
                string tmp = filePath.Remove(0, filePath.IndexOf("\\") + 1);
                Managers.Add(new Manager(tmp.Remove(tmp.Length - 4)));
            }
        }
        public void Start()
        {
            int choice = 0;
            do
            {
                Console.Clear();
                for (int i = 0; i < Managers.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Managers[i].Name}");
                }

                Console.Write("1 - выбрать\n2 - добавить\n3 - удалить\n0 - назад\n\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Выберите менеджера:");
                        int ind = int.Parse(Console.ReadLine());
                        Console.Clear();
                        SpecificManager(ind - 1);
                        break;
                    case 2:
                        Console.Write("Введите имя: ");
                        string name = Console.ReadLine();
                        if (Managers.FindAll(iter => iter.Name == name).Count != 0)
                        {
                            Console.WriteLine("Этот менеджер уже добавлен");
                            Thread.Sleep(500);
                            break;
                        }
                        AddManager(name);
                        break;
                    case 3:
                        Console.Write("Номер удаляемого менеджера:");
                        int deleteind = int.Parse(Console.ReadLine());
                        DeleteManager(deleteind - 1);
                        break;
                }
            } while (choice != 0);
        }

        public void SpecificManager(int ind)
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine($"Выбранный менеджер: {Managers[ind].Name}");
                Managers[ind].PrintTitles();
                Console.Write("1 - Выставить на продажу\n2 - Убрать с продажи\n3 - Скрыть/Раскрыть\n4 - Подробно\n5 - Прочитать сообщения\n0 - выход\n\n");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Заголовок: ");
                        string title = Console.ReadLine();
                        Console.Write("Город: ");
                        string city = Console.ReadLine();
                        Console.Write("Улица: ");
                        string street = Console.ReadLine();
                        Console.Write("Цена: ");
                        long price = long.Parse(Console.ReadLine());
                        Console.Write("Описание: ");
                        string description = Console.ReadLine();
                        Console.Write("Номер телефона: ");
                        string phoneNumber = Console.ReadLine();
                        Console.Write("Площадь(м^2): ");
                        int area = int.Parse(Console.ReadLine());
                        Managers[ind].AddProduct(new Product(title, new GEO(city, street), new Price(price), description, new Contact(Managers[ind].Name, phoneNumber), area, false));
                        break;
                    case 2:
                        Console.Write("Номер удаляемого дома:");
                        int deleteind = int.Parse(Console.ReadLine());
                        Managers[ind].DeleteProduct(deleteind);
                        break;
                    case 3:
                        Console.Write("Номер дома:");
                        int hideind = int.Parse(Console.ReadLine());
                        Managers[ind].HideProduct(hideind - 1);
                        break;
                    case 4:
                        Console.WriteLine(Managers[ind].GetInfo());
                        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
                        Console.ReadLine();
                        break;
                    case 5:
                        if (!File.Exists($@"messages\{Managers[ind].Name}.txt"))
                        {
                            Console.WriteLine("Нет сообщений");
                            Thread.Sleep(500);
                        }
                        Console.WriteLine(File.ReadAllText($@"messages\{Managers[ind].Name}.txt"));
                        Console.WriteLine("Нажмите Enter, чтобы продолжить...");
                        Console.ReadLine();
                        break;
                }
                Managers[ind].Save();
            } while (choice != 0);

        }

        public void AddManager(string name) => Managers.Add(new Manager(name));
        public void DeleteManager(int ind) => Managers.RemoveAt(ind);
    }
}
