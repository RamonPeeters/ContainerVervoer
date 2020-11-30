namespace ContainerVervoer.Logic
{
    public class Container : CargoObject
    {
        private const int EMPTY_CONTAINER_WEIGHT = 4000;

        public Container(int weight, ContainerType type) : base(EMPTY_CONTAINER_WEIGHT + weight, type) { }
    }
}
