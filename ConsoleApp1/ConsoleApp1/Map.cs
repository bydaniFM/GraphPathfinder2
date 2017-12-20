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

            int bestDistance = currentDistance;
            string bestRoute = currentRoute;

            // Mientras no encontremos una ruta óptima (global o local), repetimos,
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(currentRoute + " " + currentDistance);
            Console.BackgroundColor = ConsoleColor.Black;
            while (!optimusRoute)
            {
                bestDistance = currentDistance;
                bestRoute = currentRoute;

                // Comprobamos todas las soluciones adyacentes.
                foreach (string route in AdjacentRoutes(currentRoute))
                {
                    int nextDistance = CalculateDistance(route);
                    if (nextDistance < bestDistance)
                    {
                        bestDistance = nextDistance;
                        bestRoute = route;
                    }
                    Console.Write(route + " " + nextDistance + "\t");
                    numberOfRoutes++;
                }
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write(bestRoute + " " + bestDistance + "\t");
                Console.BackgroundColor = ConsoleColor.Black;

                if (bestRoute == currentRoute)
                {
                    optimusRoute = true;
                }
                else {
                    currentRoute = bestRoute;
                    currentDistance = bestDistance;
                }
            }
            Console.WriteLine("\nTotal Routes Explored: " + numberOfRoutes);
            Console.WriteLine("Best Solution Found:" + bestRoute + " " + bestDistance);
        }

        private string[] AdjacentRoutes(string route)
        {
            int length = route.Length;
            int numberOfAdjacents = NumberOfAdjacentRoutes(route);
            string[] routes = new string[numberOfAdjacents];

            int index = 0;
            for (int firstCity = 1; firstCity < length-2; firstCity++)
            {
                for(int secondCity = firstCity+1; secondCity < length-1; secondCity++)
                {
                    routes[index++] = StringFromSwap(route, firstCity, secondCity);
                }
            }
            if(index != numberOfAdjacents)
            {
                Console.WriteLine("Numero de rutas mal calculado");
            }
            return routes;
        }

        private string StringFromSwap(string route, int firstCity, int secondCity)
        {
            string result = "";
            char first = route[firstCity];
            char second = route[secondCity];

            result = route.Replace(first, second);
            result = result.Remove(secondCity) + first + result.Substring(secondCity + 1);

            return result;
        }

        private int NumberOfAdjacentRoutes(string route)
        {
            int count = 0;
            int max = route.Length - 3;
            for(int i = 1; i <= max; i++)
            {
                count += i;
            }
            
            return count;
        }

        private string RandomRoute ()
        {
            string route = "";
            string citiesList = alphabet.Substring(0, numberOfCities);

            //route += alphabet[0];
            for (int i = 0; i < numberOfCities; i++)
            {
                int number = rnd.Next(citiesList.Length);
                route += citiesList[number];
                citiesList = citiesList.Remove(number, 1);
            }
            route += route[0];

            return route;
        }
    }
}
