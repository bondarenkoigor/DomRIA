using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using DomRIA.DescriptionInfo;

namespace DomRIA.Users
{
    internal class Manager
    {
        public List<Product> Products { get; set; }
        public string Name { get; set; }
        public Manager(string name)
        {
            Products = new List<Product>();
            Name = name;
            Read();
        }
        public void AddProduct(Product product)
        {
            Products.Add(product);
            Save();
        }
        public void DeleteProduct(int ind)
        {
            Products.RemoveAt(ind);
            Save();
        }
        public string GetInfo()
        {
            string str = null;
            foreach (Product product in Products)
            {
                str += product.Show() + "\n";
            }
            return str;
        }
        public void PrintTitles()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Products[i].Title}");
                if (Products[i].IsHidden) Console.Write("(Скрыто)");
            }
        }

        public void HideProduct(int ind)
        {
            Products[ind].IsHidden = !Products[ind].IsHidden;
        }
        public void Save()
        {
            string str = null;
            foreach (Product product in Products)
            {
                str += product.ToString() + "\n";
            }
            if (!Directory.Exists("Houses")) Directory.CreateDirectory("Houses");
            File.WriteAllText($"Houses\\{Name}.txt", str);
        }
        public void Read()
        {
            if (!File.Exists($"Houses\\{Name}.txt") || !Directory.Exists("Houses")) return;
            string[] str = File.ReadAllLines($"Houses\\{Name}.txt");
            foreach (string line in str)
            {
                string[] arr = line.Split('|');
                AddProduct(new Product(arr[0], new GEO(arr[1], arr[2]), new Price(long.Parse(arr[3])), arr[4], new Contact(arr[5], arr[6]), int.Parse(arr[7]), bool.Parse(arr[8])));
            }
        }
    }
}