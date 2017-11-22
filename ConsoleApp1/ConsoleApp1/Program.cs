using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(7, 1000, 1000);
            map.PrintDistanceChart();

            map.BruteForce();

            Console.ReadLine();
        }
    }
}
