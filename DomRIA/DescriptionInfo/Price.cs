using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomRIA.DescriptionInfo
{
    internal class Price
    {
        public long UAH { get; set; }
        public Price(long uah)
        {
            UAH = uah;
        }
        public double ToUSD() => UAH * 30;
    }
}
