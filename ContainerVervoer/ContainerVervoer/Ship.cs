using System;
using System.Collections.Generic;


namespace ContainerVervoer
{
    public class Ship
    {
        public Container[,,] Point { get; }
        public List<Container> Containers { get; }
        private static readonly Random RndWeight = new Random();
        public int Length { get; }
        public int Width { get; }
        private long ShipWeight { get; set; }
        public long TotalWeight { get; set; }
        public long LeftWeight { get; set; }
        public long RightWeight { get; set; }
        private long SideWeightDiff10 { get; set; }

        public long SideWeightDiff20 { get; set; }
        private int ContainerCount { get; set; }
        private int CoolCount { get; set; }
        private int NormalCount { get; set; }
        private int ValueCount { get; set; }

        public Ship(long shipWeight, int length, int width, int cooled, int valued, int normal)
        {
            ShipWeight = shipWeight;
            Point = new Container[length, width, 30];
            Containers = new List<Container>();
            Length = length;
            Width = width;
            for (int i = 0; i < cooled; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 30001), "cool"));
            }
            for (int i = 0; i < valued; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 30001), "value"));
            }
            for (int i = 0; i < normal; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 30001), "normal"));
            }
        }

        public bool OrderedCorrectly()
        {
            if (((ShipWeight / 100) * 50) < TotalWeight)
            {

                if (ContainerCount == Containers.Count)
                {
                    if (BalanceCheck20())
                    {
                        Console.WriteLine("Ship as been loaded correctly");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Ship can not be balanced correctly");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Ship does not have room for all the containers");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Ship has not been loaded for at least 50%");
                return false;
            }
        }

        public long PointWeight(int column, int row)
        {
            long totalWeight = 0;
            for (int i = 0; i < 30; i++)
            {
                if (Point[row, column, i] != null)
                {
                    totalWeight += Point[row, column, i].Weight;
                }
            }
            return totalWeight;
        }

        private void AddCooledContainers()
        {
            bool added = false;
            double wtd = (double)Width / 2;
            int startwidth1;
            int startwidth2;
            if (wtd % 1 == 0)
            {
                startwidth2 = Convert.ToInt32(wtd);
                startwidth1 = Convert.ToInt32(Math.Round(wtd)) - 1;
            }
            else
            {
                startwidth1 = Convert.ToInt32(Math.Round(wtd)) - 1;
                startwidth2 = Convert.ToInt32(Math.Round(wtd)) - 1;
            }

            foreach (Container con in Containers)
            {
                if (con.Type == ContainerType.Cool)
                {
                    if (!BalanceCheck10())
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int width = startwidth1; width > -1; width--)
                            {
                                if (Point[0, width, high] == null && PointWeightCheck(0, width, con.Weight))
                                {
                                    Point[0, width, high] = con;
                                    AddWeight(width, con.Weight);
                                    added = true;
                                    break;
                                }
                            }
                            if (added)
                            {
                                added = false;
                                ContainerCount++;
                                CoolCount++;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int width = startwidth2; width < Width; width++)
                            {
                                if (Point[0, width, high] == null && PointWeightCheck(0, width,con.Weight))
                                {
                                    Point[0, width, high] = con;
                                    AddWeight(width, con.Weight);
                                    added = true;
                                    break;
                                }
                            }
                            if (added)
                            {
                                added = false;
                                ContainerCount++;
                                CoolCount++;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public void VisualPoint(int row, int column)
        {
            for (int i = 29; i > -1; i--)
            {
                if (Point[row, column, i] != null)
                {
                    Console.WriteLine(i + ": "+Point[row, column, i].Weight +  "KG Type: " + Point[row, column, i].Type.ToString());
                }
            }
        }

        private void AddNormalContainers()
        {
            bool added = false;
            double wtd = (double)Width / 2;
            int startwidth1;
            int startwidth2;
            if (wtd % 1 == 0)
            {
                startwidth2 = Convert.ToInt32(wtd);
                startwidth1 = Convert.ToInt32(Math.Round(wtd)) - 1;
            }
            else
            {
                startwidth1 = Convert.ToInt32(Math.Round(wtd)) - 1;
                startwidth2 = Convert.ToInt32(Math.Round(wtd)) - 1;
            }
            foreach (Container con in Containers)
            {
                if (con.Type != ContainerType.Cool && con.Type != ContainerType.Value)
                {
                    if (!BalanceCheck10())
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int length = 1; length < Length; length++)
                            {
                                for (int width = startwidth1; width > -1; width--)
                                {
                                    if (!added)
                                    {
                                        if (Point[length, width, high] == null && PointWeightCheck(length, width, con.Weight))
                                        {
                                            Point[length, width, high] = con;
                                            AddWeight(width, con.Weight);
                                            ContainerCount++;
                                            NormalCount++;
                                            added = true;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (added)
                                {
                                    break;
                                }
                            }
                            if (added)
                            {
                                added = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int length = 1; length < Length; length++)
                            {
                                for (int width = startwidth2; width < Width; width++)
                                {
                                    if (!added)
                                    {
                                        if (Point[length, width, high] == null && PointWeightCheck(length, width, con.Weight))
                                        {
                                            Point[length, width, high] = con;
                                            AddWeight(width, con.Weight);
                                            ContainerCount++;
                                            NormalCount++;
                                            added = true;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                if (added)
                                {
                                    break;
                                }
                            }
                            if (added)
                            {
                                added = false;
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void AddValueContainers()
        {
            bool added = false;
            double wtd = (double)Width / 2;
            int startwidth1;
            int startwidth2;
            if (wtd % 1 == 0)
            {
                startwidth2 = Convert.ToInt32(wtd);
                startwidth1 = Convert.ToInt32(Math.Round(wtd)) - 1;
            }
            else
            {
                startwidth1 = Convert.ToInt32(Math.Round(wtd)) - 1;
                startwidth2 = Convert.ToInt32(Math.Round(wtd)) - 1;
            }
            foreach (Container con in Containers)
            {
                if (con.Type == ContainerType.Value)
                {
                    if (!BalanceCheck10())
                    {
                        for (int length = 0; length < Length;)
                        {
                            if ((length + 1) % 3 != 0 || length == 0)
                            {
                                for (int high = 0; high < 29; high++)
                                {
                                    for (int width = startwidth1; width > -1; width--)
                                    {
                                        if (!added)
                                        {
                                            if (Point[length, width, high] == null && PointWeightCheck(length, width, con.Weight))
                                            {
                                                if (high != 0)
                                                {
                                                    if (Point[length, width, (high - 1)] != null)
                                                    {
                                                        if ((Point[length, width, (high - 1)].Type != ContainerType.Value))
                                                        {
                                                            Point[length, width, high] = con;
                                                            AddWeight(width, con.Weight);
                                                            ContainerCount++;
                                                            ValueCount++;
                                                            added = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Point[length, width, high] = con;
                                                    AddWeight(width, con.Weight);
                                                    ContainerCount++;
                                                    ValueCount++;
                                                    added = true;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (added)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (added)
                            {
                                added = false;
                                break;
                            }
                            else
                            {
                                length++;
                            }
                        }
                    }
                    else
                    {
                        for (int length = 0; length < Length;)
                        {
                            if ((length +1) % 3 != 0 || length == 0)
                            {
                                for (int high = 0; high < 29; high++)
                                {
                                    for (int width = startwidth2; width < Width; width++)
                                    {
                                        if (!added)
                                        {
                                            if (Point[length, width, high] == null && PointWeightCheck(length, width, con.Weight))
                                            {
                                                if (high != 0)
                                                {
                                                    if (Point[length, width, (high - 1)] != null)
                                                    {
                                                        if ((Point[length, width, (high - 1)].Type != ContainerType.Value))
                                                        {
                                                            Point[length, width, high] = con;
                                                            AddWeight(width, con.Weight);
                                                            ContainerCount++;
                                                            ValueCount++;
                                                            added = true;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    Point[length, width, high] = con;
                                                    AddWeight(width, con.Weight);
                                                    ContainerCount++;
                                                    ValueCount++;
                                                    added = true;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    if (added)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (added)
                            {
                                added = false;
                                break;
                            }
                            else
                            {
                                length++;
                            }
                        }
                    }

                }
            }
        }


        private void AddWeight(int column, long weight)
        {
            TotalWeight += weight;
            if (column == Width / 2)
            {

            }
            else if (column < ((Width / 2) - 1))
            {
                LeftWeight += weight;
            }
            else if (column > ((Width / 2) - 1))
            {
                RightWeight += weight;
            }
        }

        public bool BalanceCheck10()
        {
            SideWeightDiff10 = (TotalWeight / 100) * 10;

            SideWeightDiff20 = (TotalWeight / 100) * 20;
            if (LeftWeight - RightWeight > SideWeightDiff10)
            {
                return true;
            }
            return false;
        }

        public bool BalanceCheck20()
        {

            SideWeightDiff20 = (TotalWeight / 100) * 20;
            if (LeftWeight - RightWeight > SideWeightDiff20)
            {
                return false;
            }
            else if (RightWeight - LeftWeight > SideWeightDiff20)
            {
                return false;
            }
            return true;
        }

        private bool PointWeightCheck(int row, int column, long containerWeight)
        {
            long weight = containerWeight;
            for (int i = 0; i < 30; i++)
            {
                if (Point[row, column, i] != null)
                {
                    weight += Point[row, column, i].Weight;
                }
            }
            if (weight >= 120000)
            {
                return false;
            }
            return true;
        }

        public void OrderContainers()
        {
            AddCooledContainers();
            AddNormalContainers();
            AddValueContainers();
        }
    }
}

