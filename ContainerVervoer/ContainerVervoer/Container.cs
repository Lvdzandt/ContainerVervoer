
namespace ContainerVervoer
{
    public enum ContainerType
    {
        Cool,
        Value,
        Normal
    }
    public class Container
    {
        public int Id { get; set; }
        public long Weight { get; set; }
        public ContainerType Type { get; set; }

        public Container(int id, long weight, string type)
        {
            Id = id;
            Weight = weight;
            switch (type)
            {
                case "cool":
                    Type = ContainerType.Cool;
                    break;
                case "value":
                    Type = ContainerType.Value;
                    break;
                case "normal":
                    Type = ContainerType.Normal;
                    break;
            }
        }
    }
}
