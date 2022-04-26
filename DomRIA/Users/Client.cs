using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DomRIA.DescriptionInfo;

namespace DomRIA.Users
{
    internal class Client
    {
        public List<Product> Products { get; set; }

        public Client()
        {
            Products = new List<Product>();
            if (!Directory.Exists("Houses")) return;
            foreach (string filePath in Directory.EnumerateFiles("Houses", "*.txt"))
            {
                string[] str = File.ReadAllLines(filePath);
                foreach (string line in str)
                {
                    string[] arr = line.Split('|');
                    if(!bool.Parse(arr[8])) Products.Add(new Product(arr[0], new GEO(arr[1], arr[2]), new Price(long.Parse(arr[3])), arr[4], new Contact(arr[5], arr[6]), int.Parse(arr[7]), bool.Parse(arr[8])));
                }
            }
        }
        
        public void ShowAll()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{i + 1}. ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Products[i].Show());
            }
        }
    }
}
