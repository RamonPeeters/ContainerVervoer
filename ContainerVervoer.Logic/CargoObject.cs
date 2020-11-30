namespace ContainerVervoer.Logic
{
    public abstract class CargoObject
    {
        public int Weight { get; }
        public ContainerType Type { get; }

        public abstract int GetMaxWeightOnTop();

        protected CargoObject(int weight, ContainerType type)
        {
            Weight = weight;
            Type = type;
        }
    }
}
