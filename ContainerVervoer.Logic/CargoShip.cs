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

        public ContainerRow[] Divide(List<Container> containers)
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
            List<Container> sortedContainers = containers.OrderBy(v => v.Type.HasFlag(ContainerType.Coolable)).ThenBy(v => v.Type.HasFlag(ContainerType.Valuable)).ThenByDescending(v => v.Weight).ToList();

            // Loop over all sorted containers
            for (int i = 0; i < sortedContainers.Count; i++)
            {
                bool successful = false;

                // Sort rows by least current containers
                // We want to fill the one with the least containers first
                ContainerRow[] sortedRows = rows.OrderBy(r => r.GetTotalContainers()).ToArray();

                // Loop over all rows
                for (int j = 0; j < sortedRows.Length; j++)
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
                    if (sortedRows[j].TryAdd(sortedContainers[i]))
                    {
                        // Successful, look at next container
                        successful = true;
                        break;
                    }
                }

                // Was not able to add container anywhere
                if (!successful)
                {
                    throw new Exception($"Could not add container to ship!");
                }

                // Successful
            }

            return rows;
        }
    }
}
