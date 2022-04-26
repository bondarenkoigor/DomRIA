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
    internal class MainMenu : IMenu
    {
        public ClientMenu Client { get; set; }
        public ManagerMenu Manager { get; set; }
        public MainMenu()
        {
            Client = new ClientMenu();
            Manager = new ManagerMenu();
        }

        public void Start()
        {
            do
            {
                Console.Clear();
                Console.Write("1 - Я клиент\n2 - менеджеры\n");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1) Client.Start();
                else if (choice == 2) Manager.Start();
            } while (true);
        }
    }
}
