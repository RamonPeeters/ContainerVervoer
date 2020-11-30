using System.Collections.Generic;

namespace ContainerVervoer.Logic
{
    public class ContainerColumn
    {
        // X....
        // X....
        // X....
        // X....
        // X....


        private readonly List<CargoObject> Containers;

        public ContainerColumn()
        {
            // Initialise
            Containers = new List<CargoObject>();
        }

        public bool TryAdd(CargoObject container)
        {
            // Maximum weight
            // The bottom container will have the most weight on top of them, so we only have to check that one.
            // If that one does not fail, none will.
            // [!] objects may have different "max weights"
            if (GetTotalWeightWithContainer(container) > CargoObject.MAX_WEIGHT_ON_TOP)
            {
                return false;
            }






            // Valuable, must be on top
            if (container.Type.HasFlag(ContainerType.Valuable))
            {
                Containers.Add(container);
            }
            return true;
        }

        private int GetTotalWeightWithContainer(CargoObject container)
        {
            int totalWeight = container.Weight;

            for (int i = 0; i < Containers.Count; i++)
            {
                totalWeight += Containers[i].Weight;
            }

            return totalWeight;
        }
    }
}
