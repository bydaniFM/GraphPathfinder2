using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class City
    {
        public int x, y;
        public string name;

        public City (int _x, int _y, string _name)
        {
            x = _x;
            y = _y;
            name = _name;
        }

        public City (string _name, Random rnd, int maxX, int maxY)
        {
            x = rnd.Next(maxX);
            y = rnd.Next(maxY);
            name = _name;
        }

        public int DistanceToCity (City cityTo)
        {
            int vertical = this.y - cityTo.y;
            int horizontal = this.x - cityTo.x;
            return (int) Math.Sqrt(Math.Pow(horizontal, 2) + Math.Pow(vertical, 2));
        }
    }
}
