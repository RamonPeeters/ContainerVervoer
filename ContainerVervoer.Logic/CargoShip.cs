﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerVervoer.Logic
{
    public class CargoShip
    {
        private readonly int Width;
        private readonly int Length;
        private readonly int MaxWeight;
        private ContainerRow[] Rows;

        /// <summary>
        /// Initialises a new cargo ship.
        /// </summary>
        /// <param name="width">The number of rows on the ship.</param>
        /// <param name="length">The number of columns on the ship.</param>
        /// <param name="maxWeight">The maximum weight the ship can carry.</param>
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

            if (containers.Any(c => (double)c.Weight / totalWeight > 0.7d))
            {
                throw new Exception("Unbalanced because one container is too heavy");
            }

            ContainerRow[] rows = new ContainerRow[Width];
            for (int i = 0; i < Width; i++) rows[i] = new ContainerRow(Length);

            List<Container> sortedContainers = containers.OrderByDescending(v => v.Type.HasFlag(ContainerType.Coolable)).ThenByDescending(v => v.Type.HasFlag(ContainerType.Valuable)).ThenByDescending(v => v.Weight).ToList();

            foreach (var a in sortedContainers)
            {
                Console.WriteLine($"Type: {a.Type}, Weight: {a.Weight}");
            }

            Console.WriteLine("\n\n");

            for (int i = 0; i < sortedContainers.Count; i++)
            {
                bool successful = false;

                // Sort rows by least current containers, and keep original indices (since we need them later on for a check)
                List<KeyValuePair<ContainerRow, int>> a = rows.Select((x, i) => new KeyValuePair<ContainerRow, int>(x, i))
                                                              .OrderBy(r => r.Key.GetTotalContainers())
                                                              .ToList();
                ContainerRow[] sortedRows = a.Select(x => x.Key).ToArray();
                //ContainerRow[] sortedRows = rows;
                int[] originalIndices = a.Select(x => x.Value).ToArray();

                for (int j = 0; j < sortedRows.Length; j++)
                {
                    if (TryAddContainer(rows, originalIndices[j], sortedContainers[i]))
                    {
                        successful = true;
                        break;
                    }
                }

                if (!successful) throw new Exception($"Could not add container (Type: {sortedContainers[i].Type}) to ship!");
            }

            Rows = rows;
            return rows;
        }

        public double GetLeftWeightRatio()
        {
            return (double)GetTotalWeightLeft(Rows) / GetTotalWeight(Rows);
        }

        private int GetTotalWeight(ContainerRow[] rows)
        {
            int total = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                total += rows[i].GetTotalWeight();
            }
            return total;
        }

        private int GetTotalWeightLeft(ContainerRow[] rows)
        {
            int total = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                total += rows[i].GetTotalWeightLeft();
            }
            return total;
        }

        private int GetTotalWeightRight(ContainerRow[] rows)
        {
            int total = 0;
            for (int i = 0; i < rows.Length; i++)
            {
                total += rows[i].GetTotalWeightRight();
            }
            return total;
        }

        private bool TryAddContainer(ContainerRow[] rows, int index, Container container)
        {
            if (container.Type.HasFlag(ContainerType.Coolable) && index != 0) return false;
            if (container.Type.HasFlag(ContainerType.Valuable) && index != 0 && index != rows.Length - 1) return false;

            int totalLeft = GetTotalWeightLeft(rows);
            int totalRight = GetTotalWeightRight(rows);
            return rows[index].TryAdd(container, totalLeft, totalRight);
        }
    }
}
