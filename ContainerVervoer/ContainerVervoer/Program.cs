using System;

namespace ContainerVervoer
{
    class Program
    {

        
        static void Main(string[] args)
        {
            Ship ship;
            int ShipLength = 0;
            int ShipWidth = 0;
            int CooledContainers = 0;
            int NormalContainers = 0;
            int ValuebleContainers = 0;
            bool Startup = false;
            while(!Startup)
            {
                try
                {
                    Console.WriteLine("How long is the ship? (measured in containers)");
                    ShipLength = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("How wide is the ship? (measured in containers)");
                    ShipWidth = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("The ship is " + ShipLength + " Long, and " + ShipWidth + " Wide. Is this correct(y/n)");
                    if (Convert.ToString(Console.ReadLine()) == "y")
                    {
                        Console.WriteLine("How many Cooled containers are on ship?");
                        CooledContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many Valueble containers are on ship?");
                        ValuebleContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("How many Normal containers are on ship?");
                        NormalContainers = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Regular: " + NormalContainers + " Valued: " + ValuebleContainers + " Cooled:" + CooledContainers + ". I this correct (y/n)");
                        if (Convert.ToString(Console.ReadLine()) == "y")
                        {
                            
                            Startup = true;
                        }
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException Exception)
                {
                    Console.WriteLine("Wrong input : " + Exception.Message);
                }
                
            }
            ship = new Ship(ShipLength, ShipWidth, CooledContainers, ValuebleContainers, NormalContainers);
            ship.OrderContainers();
            if (ship.BalanceCheck())
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
