using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer.Logic
{
    public class ContainerColumn
    {
        private readonly List<Container> Containers;

        public ContainerColumn()
        {
            // Initialise
            Containers = new List<Container>();
        }

        public Container this[int index] { get { return Containers[index]; } }

        public bool TryAdd(Container container)
        {
            if (!MayPlaceOnTopOfContainer(0, container)) return false;

            if (Containers.Any(c => c.Type.HasFlag(ContainerType.Valuable)))
            {
                if (container.Type.HasFlag(ContainerType.Valuable)) return false;

                Containers.Insert(Math.Max(Containers.Count - 2, 0), container);
                return true;
            } else
            {
                Containers.Add(container);
                return true;
            }
        }

        public int GetTotalWeight()
        {
            return Containers.Sum(c => c.Weight);
        }

        private bool MayPlaceOnTopOfContainer(int containerIndex, Container withContainer)
        {
            int totalWeight = withContainer.Weight;
            for (int i = containerIndex + 1; i < Containers.Count; i++)
            {
                totalWeight += Containers[i].Weight;
            }

            return totalWeight <= Container.GetMaxWeightOnTop();
        }

        public override string ToString()
        {
            string s = $"Weight: {GetTotalWeight():#0}, ";

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
