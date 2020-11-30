namespace ContainerVervoer.Logic
{
    public class Container : CargoObject
    {
        private const int MAX_WEIGHT_ON_TOP = 120000000;
        private const int EMPTY_CONTAINER_WEIGHT = 4000;

        public Container(int weight, ContainerType type) : base(EMPTY_CONTAINER_WEIGHT + weight, type) { }

        public override int GetMaxWeightOnTop()
        {
            return MAX_WEIGHT_ON_TOP;
        }
    }
}
