using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class Ship
    {
        private Container[,,] Point { get; set; }
        List<Container> Containers { get; set; }
        private static readonly Random RndWeight = new Random();
        private int Length { get; set; }
        private int Width { get; set; }
        private long TotalWeight { get; set; }
        private long LeftWeight { get; set; }
        private long RightWeight { get; set; }
        private long SideWeightDiff10 { get; set; }

        private long SideWeightDiff20 { get; set; }
        private int ContainerCount { get; set; }

        public Ship(int _length, int _width, int _cooled, int _valued, int _normal)
        {
            Point = new Container[_length, _width, 30];
            Containers = new List<Container>();
            Length = _length;
            Width = _width;
            for (int i = 0; i < _cooled; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 31000), "cool"));
            }
            for (int i = 0; i < _valued; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 31000), "value"));
            }
            for (int i = 0; i < _normal; i++)
            {
                Containers.Add(new Container((Containers.Count + 1), RndWeight.Next(4000, 31000), ""));
            }
        }

        private void AddCooledContainers()
        {
            bool added = false;
            foreach (Container con in Containers)
            {
                if (con.IsCooled)
                {
                    for (int high = 0; high < 30; high++)
                    {
                        for (int width = 0; width < Width; width++)
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

        private void AddNormalContainers()
        {
            bool added = false;
            double wtd = (double)Width / 2;
            int startwidth = Convert.ToInt32(Math.Round(wtd)) -1;
            foreach (Container con in Containers)
            {
                if (!con.IsCooled && !con.IsValued)
                {
                    if (!BalanceCheck())
                    {
                        for (int high = 0; high < 30; high++)
                        {
                            for (int length = 1; length < Length; length++)
                            {
                                for (int width =  startwidth; width < 0; width--)
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


        private void AddWeight(int Column, long weight)
        {
            TotalWeight += weight;
            if (Column < (Width / 2))
            {
                LeftWeight += weight;
            }
            else if (Column > (Width / 2))
            {
                RightWeight += weight;
            }
        }

        public bool BalanceCheck()
        {
            SideWeightDiff10 = (TotalWeight/100) * 10;

            SideWeightDiff20 = (TotalWeight / 100) * 20;
            if (LeftWeight - RightWeight > SideWeightDiff10)
            {
                return false;
            }
            return true;
        }

        private bool PointWeightCheck(int row, int colum)
        {
            long weight = 0;
            for (int i = 0; i < 30; i++)
            {
                if (Point[row, colum, i] != null)
                {
                    weight += Point[row, colum, i].Weight;
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

