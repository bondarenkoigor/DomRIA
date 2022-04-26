using System;
using DomRIA.DescriptionInfo;
using DomRIA.Users;
using DomRIA.Menus.Classes;

namespace DomRIA
{
    internal class Program
    {
        static void Main(string[] args)
        {
           MainMenu main = new MainMenu();
           main.Start();
        }
    }
}
