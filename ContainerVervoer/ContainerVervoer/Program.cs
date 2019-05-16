using System;
using System.Transactions;

namespace ContainerVervoer
{
    class Program
    {


        static void Main(string[] args)
        {
            Ship ship;
            int shipLength = 0;
            int shipWidth = 0;
            int shipWeight = 0;
            int cooledContainers = 0;
            int normalContainers = 0;
            int valuableContainers = 0;
            bool startup = false;
            while (!startup)
            {
                try
                {
                    Console.WriteLine("How heavy is the ship (measured in kg)");
                    shipWeight = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("How long is the ship? (measured in containers)");
                    shipLength = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("How wide is the ship? (measured in containers)");
                    shipWidth = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("The ship is " + shipLength + " Long, and " + shipWidth +
                                      " Wide. Is this correct(y/n)");
                    if (Convert.ToString(Console.ReadLine()) == "y")
                    {
                        Console.WriteLine("How many Cooled containers are on ship?");
                        cooledContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many Valuable containers are on ship?");
                        valuableContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many Normal containers are on ship?");
                        normalContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Regular: " + normalContainers + " Valued: " + valuableContainers +
                                          " Cooled:" + cooledContainers + ". I this correct (y/n)");
                        if (Convert.ToString(Console.ReadLine()) == "y")
                        {
                            startup = true;
                        }
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException exception)
                {
                    Console.WriteLine("Wrong input : " + exception.Message);
                }

            }

            ship = new Ship(shipWeight, shipLength, shipWidth, cooledContainers, valuableContainers, normalContainers);
            ship.OrderContainers();
            if (ship.OrderedCorrectly())
            {
                while (true)
                {
                    try
                    {

                        Console.Clear();
                        ShowShip(ship);
                        Console.WriteLine("Enter [RowNumber,ColumnNumber] for more information about that point");
                        string point = Console.ReadLine();
                        int row = Convert.ToInt32(point.Substring(1, (point.LastIndexOf(',') - 1)));
                        int column = Convert.ToInt32(point.Substring((point.LastIndexOf(',') + 1), (point.LastIndexOf(']') - 3)));
                        ship.VisualPoint(row, column);
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            Console.ReadLine();
        }


        private static void ShowShip(Ship ship)
        {

            Console.WriteLine("TotalWeight: " + ship.TotalWeight);
            Console.WriteLine("LeftWeight: " + ship.LeftWeight);
            Console.WriteLine("RightWeight: " + ship.RightWeight);
            Console.WriteLine("Max Difference: " + ship.SideWeightDiff20);
            Console.Write("          ");
            for (int i = 0; i < ship.Width; i++)
            {
                Console.Write("       " + i);
            }

            Console.WriteLine("");

            for (int j = 0; j < ship.Length; j++)
            {
                Console.Write("          " + j);
                for (int i = 0; i < ship.Width; i++)
                {
                    long weight = ship.PointWeight(i, j);
                    if (weight.ToString().Length == 6)
                    {
                        Console.Write("[" + weight + "] ");
                    }
                    else
                    {
                        Console.Write("[" + weight + " ] ");
                    }

                }

                Console.WriteLine("");
            }
        }

    }
}

