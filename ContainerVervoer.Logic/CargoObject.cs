namespace ContainerVervoer.Logic
{
    public abstract class CargoObject
    {
        public const int MAX_WEIGHT_ON_TOP = 120000000;
        public int Weight { get; }
        public ContainerType Type { get; }

        protected CargoObject(int weight, ContainerType type)
        {
            Weight = weight;
            Type = type;
        }
    }
}
