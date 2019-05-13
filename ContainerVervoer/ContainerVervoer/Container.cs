
namespace ContainerVervoer
{
    public class Container
    {
        public int Id { get; set; }
        public long Weight { get; set; }
        public bool IsCooled { get; set; }
        public bool IsValued { get; set; }

        public Container(int id, long weight, string type)
        {
            Id = id;
            Weight = weight;
            if (type == "cool")
            {
                IsCooled = true;
                IsValued = false;
            }
            else if (type == "value")
            {
                IsCooled = false;
                IsValued = true;
            }
            else if (type == "normal")
            {
                IsCooled = false;
                IsValued = false;
            }
        }
    }
}
