using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer.Logic
{
    public class CargoShip
    {
        private readonly int Width;
        private readonly int Length;
        private readonly int MaxWeight;

        public CargoShip(int width, int length, int maxWeight)
        {
            Width = width;
            Length = length;
            MaxWeight = maxWeight;
        }

        public void Divide(List<CargoObject> containers)
        {
            // Verify total weight is sufficient
            int totalWeight = containers.Sum(c => c.Weight);
            if (totalWeight > MaxWeight) throw new Exception("Too much weight");
            if (totalWeight < MaxWeight / 2.0d) throw new Exception("Not enough weight");

            // Initialise and populate
            ContainerRow[] rows = new ContainerRow[Width];
            for (int i = 0; i < Width; i++) rows[i] = new ContainerRow(Length);

            // Sort
            // First by coolable (should always be in the first row)
            // Then by valuable (should be either in the first row or the last row)
            List<CargoObject> sortedContainers = containers.OrderBy(v => v.Type.HasFlag(ContainerType.Coolable)).ThenBy(v => v.Type.HasFlag(ContainerType.Valuable)).ToList();

            for (int i = 0; i < sortedContainers.Count; i++)
            {
                bool successful = false;
                for (int j = 0; j < rows.Length; j++)
                {
                    // Is coolable and not in front
                    if (sortedContainers[i].Type.HasFlag(ContainerType.Coolable) && j != 0)
                    {
                        // Invalid position
                        continue;
                    }

                    // Is valuable and not in front or back
                    if (sortedContainers[i].Type.HasFlag(ContainerType.Valuable) && !(j == 0 || j == rows.Length - 1))
                    {
                        // Invalid position
                        continue;
                    }

                    // Valid position, try to add
                    if (rows[j].TryAdd(sortedContainers[i]))
                    {
                        // Successful, look at next container
                        successful = true;
                        break;
                    }
                }

                // Unsuccessful, throw
                if (!successful)
                {
                    // Well, well, well... Look what we've got here!
                    throw new Exception($"Could not add container");
                }

                // Successful
            }
        }
    }
}
