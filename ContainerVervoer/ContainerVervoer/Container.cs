using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    class Container
    {
        public int ID { get; set; }
        public long Weight { get; set; }
        public bool IsCooled { get; set; }
        public bool IsValued { get; set; }

        public Container(int _id, long _weight, string _type)
        {
            ID = _id;
            Weight = _weight;
            if (_type == "cool")
            {
                IsCooled = true;
                IsValued = false;
            }
            else if (_type == "value")
            {
                IsCooled = false;
                IsValued = true;
            }
            else
            {
                IsCooled = false;
                IsValued = false;
            }
        }
    }
}
