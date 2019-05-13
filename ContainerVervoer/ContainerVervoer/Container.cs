
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
            switch (type)
            {
                case "cool":
                    IsCooled = true;
                    IsValued = false;
                    break;
                case "value":
                    IsCooled = false;
                    IsValued = true;
                    break;
                case "normal":
                    IsCooled = false;
                    IsValued = false;
                    break;
            }
        }
    }
}
