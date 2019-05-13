﻿using System;

namespace ContainerVervoer
{
    class Program
    {

        
        static void Main(string[] args)
        {
            Ship ship;
            int shipLength = 0;
            int shipWidth = 0;
            int cooledContainers = 0;
            int normalContainers = 0;
            int valuableContainers = 0;
            bool startup = false;
            while(!startup)
            {
                try
                {
                    Console.WriteLine("How long is the ship? (measured in containers)");
                    shipLength = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("How wide is the ship? (measured in containers)");
                    shipWidth = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("The ship is " + shipLength + " Long, and " + shipWidth + " Wide. Is this correct(y/n)");
                    if (Convert.ToString(Console.ReadLine()) == "y")
                    {
                        Console.WriteLine("How many Cooled containers are on ship?");
                        cooledContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many Valuable containers are on ship?");
                        valuableContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many Normal containers are on ship?");
                        normalContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Regular: " + normalContainers + " Valued: " + valuableContainers + " Cooled:" + cooledContainers + ". I this correct (y/n)");
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
            ship = new Ship(shipLength, shipWidth, cooledContainers, valuableContainers, normalContainers);
            ship.OrderContainers();
            if (ship.BalanceCheck20())
            {
                Console.WriteLine("Ship has been loaded");
            }
            else
            {
                Console.WriteLine("Ship is not balanced");
            }
            Console.ReadLine();


        }
    }
}
