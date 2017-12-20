using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(52, 1000, 1000);
            map.PrintDistanceChart();

            //map.BruteForce();
            Console.WriteLine("---------------------------------");
            map.HillClimbing();
            Console.WriteLine("---------------------------------");
            map.HillClimbing();
            Console.WriteLine("---------------------------------");
            map.HillClimbing();
            Console.WriteLine("---------------------------------");
            Console.WriteLine("END");

            Console.ReadLine();
        }
    }
}
