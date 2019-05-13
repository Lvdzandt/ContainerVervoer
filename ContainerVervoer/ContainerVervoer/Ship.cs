using System;
using System.Collections.Generic;


namespace ContainerVervoer
{
    public class Ship
    {
        private Container[,,] Point { get; }
        public List<Container> Containers { get; }
        private static readonly Random RndWeight = new Random();
        private int Length { get; }
        private int Width { get; }
        private long TotalWeight { get; set; }
        private long LeftWeight { get; set; }
        private long RightWeight { get; set; }
        private long SideWeightDiff10 { get; set; }

        public long SideWeightDiff20 { get; set; }
        private int ContainerCount { get; set; }

        public Ship(int length, int width, int cooled, int valued, int normal)
        {
            Point = new Container[length, width, 30];
            Containers = new List<Container>();
            Length = length;
            Width = width;
            for (int i = 0; i < cooled; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 31000), "cool"));
            }
            for (int i = 0; i < valued; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 31000), "value"));
            }
            for (int i = 0; i < normal; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 31000), "normal"));
            }
        }

        private void AddCooledContainers()
        {
            bool added = false;
            double wtd = (double)Width / 2;
            int startwidth = Convert.ToInt32(Math.Round(wtd)) - 1;
            foreach (Container con in Containers)
            {
                if (con.IsCooled)
                {
                    if (!BalanceCheck10())
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int width = startwidth; width > 0; width--)
                            {
                                if (Point[0, width, high] == null && PointWeightCheck(0, width))
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
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int width = startwidth; width < Width; width++)
                            {
                                if (Point[0, width, high] == null && PointWeightCheck(0, width))
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
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void AddNormalContainers()
        {
            bool added = false;
            double wtd = (double)Width / 2;
            int startwidth = Convert.ToInt32(Math.Round(wtd)) -1;
            foreach (Container con in Containers)
            {
                if (!con.IsCooled && !con.IsValued)
                {
                    if (!BalanceCheck10())
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int length = 1; length < Length; length++)
                            {
                                for (int width =  startwidth; width > 0; width--)
                                {
                                    if (!added)
                                    {
                                        if (Point[length, width, high] == null && PointWeightCheck(length, width))
                                        {
                                            Point[length, width, high] = con;
                                            AddWeight(width, con.Weight);
                                            ContainerCount++;
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
                                for (int width = startwidth; width < Width; width++)
                                {
                                    if (!added)
                                    {
                                        if (Point[length, width, high] == null && PointWeightCheck(length, width))
                                        {
                                            Point[length, width, high] = con;
                                            AddWeight(width, con.Weight);
                                            ContainerCount++;
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
            foreach (Container con in Containers)
            {
                if (con.IsValued)
                {
                    for (int length = 0; length < Length; length++)
                    {
                        if (length % 3 != 0)
                        {
                            for (int high = 0; high < 29; high++)
                            {
                                for (int width = 1; width < Width; width++)
                                {
                                    if (Point[length, high, width] == null && PointWeightCheck(length, high))
                                    {
                                        Point[length, width, high] = con;
                                        AddWeight(high, con.Weight);
                                        ContainerCount++;
                                        added = true;
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
                            length++;
                        }
                    }
                }
            }
        }


        private void AddWeight(int column, long weight)
        {
            TotalWeight += weight;
            
            if (column < ((Width / 2)-1))
            {
                LeftWeight += weight;
            }
            else if (column > ((Width / 2)-1))
            {
                RightWeight += weight;
            }
        }

        public bool BalanceCheck10()
        {
            SideWeightDiff10 = (TotalWeight/100) * 10;

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
            }else if (RightWeight - LeftWeight > SideWeightDiff20)
            {
                return false;
            }
            return true;
        }

        private bool PointWeightCheck(int row, int column)
        {
            long weight = 0;
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
            //AddValueContainers();
        }
    }
}

