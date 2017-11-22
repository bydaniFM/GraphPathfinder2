using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Map
    {
        public City[] cities;
        public int[,] distances;
        public int numberOfCities;
        public int maxX, maxY;
        const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private Random rnd;

        // BRUTE FORCE
        private int bestDistance;
        private string bestRoute;
        private int numberOfRoutes;

        public Map(int _numberOfCities, int _maxX, int _maxY)
        {
            maxX = _maxX;
            maxY = _maxY;
            rnd = new Random();
            numberOfCities = _numberOfCities;
            distances = new int[numberOfCities, numberOfCities];
            cities = new City[numberOfCities];

            for (int city = 0; city < numberOfCities; city++)
            {
                cities[city] = new City("" + alphabet[city], rnd, maxX, maxY);
            }

            for (int cityFrom = 0; cityFrom < numberOfCities; cityFrom++)
            {
                for (int cityTo = 0; cityTo < numberOfCities; cityTo++)
                {
                    distances[cityFrom, cityTo] = cities[cityFrom].DistanceToCity(cities[cityTo]);
                }
            }
        }

        public void PrintDistanceChart()
        {
            Console.Write("\t");
            for (int city = 0; city < numberOfCities; city++)
            {
                Console.Write(cities[city].name + "\t");
            }
            Console.Write("\n");
            for (int cityFrom = 0; cityFrom < numberOfCities; cityFrom++)
            {
                Console.Write(cities[cityFrom].name + "\t");
                for (int cityTo = 0; cityTo < numberOfCities; cityTo++)
                {
                    Console.Write(distances[cityFrom, cityTo] + "\t");
                }
                Console.Write("\n");
            }
        }

        public void BruteForce()
        {
            string citiesList = alphabet.Substring(0, numberOfCities);

            bestDistance = int.MaxValue;
            bestRoute = "";
            numberOfRoutes = 0;

            DateTime dateBefore = DateTime.Now;
            BruteForcePermutation("" + citiesList[0], citiesList.Substring(1), "" + citiesList[0]);

            DateTime dateAfter = DateTime.Now;

            Console.WriteLine("\n\nBRUTE FORCE");
            Console.WriteLine("Number of routes explored: " + numberOfRoutes);
            Console.WriteLine("Time Ellapsed: " + (dateAfter - dateBefore).TotalSeconds);
            Console.WriteLine("Best route: " + bestRoute);
            Console.WriteLine("Best distance: " + bestDistance);
        }

        private void BruteForcePermutation (string begin, string body, string end)
        {
            int length = body.Length;

            if (length == 1)
            {
                string currentRoute = begin + body + end;
                int currentDistance = CalculateDistance(currentRoute);
                if (currentDistance < bestDistance)
                {
                    bestRoute = currentRoute;
                    bestDistance = currentDistance;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                numberOfRoutes++;

                Console.Write(currentRoute + " " + currentDistance + "\t");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    string newBody = body.Remove(i, 1);
                    BruteForcePermutation(begin + body[i], newBody, end);
                }
            }
        }

        private int CalculateDistance (string route)
        {
            int distance = 0;
            int length = route.Length;

            for (int i = 0; i < length - 1; i++)
            {
                int indexFrom = alphabet.IndexOf(route[i]);
                int indexTo = alphabet.IndexOf(route[i+1]);
                distance += distances[indexFrom, indexTo];
            }

            return distance;
        }

        public void HillClimbing()
        {
            // Elegir una posición de salida, es decir, generar una ruta aleatoria.
            string currentRoute = RandomRoute();
            int currentDistance = CalculateDistance(currentRoute);
            numberOfRoutes = 1;
            bool optimusRoute = false;

            // Mientras no encontremos una ruta óptima (global o local), repetimos,
            while (!optimusRoute)
            {
                // Comprobamos todas las soluciones adyacentes.
                foreach (string route in AdjacentRoutes(currentRoute))
                {

                }

                // Si ninguna de ellas mejora nuestra solución actual,
                    // Consideramos que ya hemos encontrado una solución óptima.
                // En caso contrario,
                    // Nos quedamos con la mejor solución adyacente encontrada
            }

        }

        private string RandomRoute ()
        {
            string route = "";
            string citiesList = alphabet.Substring(1, numberOfCities);

            route += alphabet[0];
            for (int i = 0; i < numberOfCities - 1; i++)
            {
                int number = rnd.Next(citiesList.Length);
                route += alphabet[number];
                citiesList = citiesList.Remove(number, 1);
            }
            route += alphabet[0];

            return route;
        }
    }
}
