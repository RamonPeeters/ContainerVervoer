using System;
using System.Linq;

namespace ContainerVervoer.Logic
{
    public class ContainerRow
    {
        private readonly ContainerColumn[] Columns;

        public ContainerRow(int columns)
        {
            // Initialise and populate
            Columns = new ContainerColumn[columns];
            for (int i = 0; i < columns; i++) Columns[i] = new ContainerColumn();
        }

        public bool TryAdd(Container container, double totalLeft, double totalRight)
        {
            double leftTotalRatio = totalLeft / (totalLeft + totalRight);
            if (leftTotalRatio > 0.5d)
            {
                return TryAddRight(container);
            } else
            {
                return TryAddLeft(container);
            }
        }

        private bool TryAddLeft(Container container)
        {
            ContainerColumn[] sortedLeftColumns = Columns.Where((x, i) => i < Columns.Length / 2).OrderBy(x => x.GetTotalWeight()).ToArray();
            for (int i = 0; i < sortedLeftColumns.Length; i++)
            {
                if (sortedLeftColumns[i].TryAdd(container)) return true;
            }
            return TryAddCentre(container);
        }

        private bool TryAddRight(Container container)
        {
            ContainerColumn[] sortedRightColumns = Columns.Where((x, i) => i > Columns.Length / 2).OrderBy(x => x.GetTotalWeight()).ToArray();
            for (int i = 0; i < sortedRightColumns.Length; i++)
            {
                if (sortedRightColumns[i].TryAdd(container)) return true;
            }
            return TryAddCentre(container);
        }

        private bool TryAddCentre(Container container)
        {
            if (Columns.Length % 2 == 0) return false;
            else return Columns[Columns.Length / 2].TryAdd(container);
        }

        public int GetTotalWeightLeft()
        {
            int total = 0;
            for (int i = 0; i < Columns.Length / 2; i++)
            {
                total += Columns[i].GetTotalWeight();
            }
            return total;
        }

        public int GetTotalWeightRight()
        {
            int total = 0;
            for (int i = 0; i < Columns.Length / 2; i++)
            {
                total += Columns[^(i + 1)].GetTotalWeight();
            }
            return total;
        }

        public int GetTotalWeightCentre()
        {
            if (Columns.Length % 2 == 0) return 0;
            return Columns[Columns.Length / 2].GetTotalWeight();
        }

        public override string ToString()
        {
            string s = "";

            foreach (ContainerColumn column in Columns)
            {
                s += column.ToString() + "\n";
            }

            return s;
        }

        public int GetTotalContainers()
        {
            return Columns.Sum(c => c.GetTotalContainers());
        }
    }
}
