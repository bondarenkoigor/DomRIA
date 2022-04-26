using System;
using System.Collections.Generic;
using System.Linq;
using DomRIA.DescriptionInfo;

namespace DomRIA
{
    internal class Product
    {
        public string Title { get; set; }
        public GEO Geo { get; set; }
        public Price Price { get; set; }
        public string Text { get; set; }
        public Contact Owner { get; set; }
        public int Area { get; set; }
        public bool IsHidden { get; set; }
        
        public Product(string title, GEO geo, Price price, string text, Contact contact, int area, bool isHidden) 
        {
            Title = title;
            Geo = geo;
            Price = price;
            Text = text;
            Owner = contact;
            Area = area;
            IsHidden = isHidden;
        }

        public double DollarsPerMeter() => Price.ToUSD() / Area;

        public string Show()
        {
            string str = String.Format("{0}\n{1}, {2}\n{3}грн ${4} ${5}на кв. м.\n{6}\n{7}, {8}\n{9} кв.м.\n", Title, Geo.City, Geo.Street, Price.UAH, Price.ToUSD(), DollarsPerMeter(), Text, Owner.Name, Owner.PhoneNumber, Area);
            return str;
        }

        public override string ToString()
        {
            return String.Format("{0}|{1}|{2}|{3}|{4:00}|{5}|{6}|{7}|{8}", Title, Geo.City, Geo.Street, Price.UAH, Text, Owner.Name, Owner.PhoneNumber, Area, IsHidden);
        }
    }
}
