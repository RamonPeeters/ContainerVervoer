using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer.Logic
{
    public class ContainerColumn
    {
        // X....
        // X....
        // X....
        // X....
        // X....


        private readonly List<Container> Containers;

        public ContainerColumn()
        {
            // Initialise
            Containers = new List<Container>();
        }

        public bool TryAdd(Container container)
        {
            /*
             * If we know the container is VALUABLE, it has to be on TOP
             * 
             * 
             * 
             * 
             */


            // Check weight
            if (!MayPlaceOnTopOfContainer(0, container)) return false;

            // From this point on we know the weight is sufficient, wherever we place it.
            // Already has a valuable container
            if (Containers.Any(c => c.Type.HasFlag(ContainerType.Valuable)))
            {
                // Cannot have more than one valuable, since they cannot be placed on top of each other.
                if (container.Type.HasFlag(ContainerType.Valuable)) return false;

                // Otherwise, insert directly below the last one
                Containers.Insert(Containers.Count - 2, container);
                return true;
            } else
            {
                // Add on top
                Containers.Add(container);
                return true;
            }
        }

        private bool MayPlaceOnTopOfContainer(int containerIndex, Container withContainer)
        {
            // Get total weight on top of container, including the one to insert
            int totalWeight = withContainer.Weight;
            for (int i = containerIndex + 1; i < Containers.Count; i++)
            {
                totalWeight += Containers[i].Weight;
            }

            // The total new weight on top is less than or equal to max weight on top
            return totalWeight <= Container.GetMaxWeightOnTop();
        }

        public override string ToString()
        {
            string s = "";

            foreach (Container container in Containers)
            {
                s += container.ToString() + ", ";
            }

            return s;
        }

        public int GetTotalContainers()
        {
            return Containers.Count;
        }
    }
}
