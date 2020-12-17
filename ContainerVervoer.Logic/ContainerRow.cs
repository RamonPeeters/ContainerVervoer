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
            // double leftTotalRatio
            //
            // Het schip moet in evenwicht zijn: het volledige gewicht van de containers voor iedere helft mag niet meer dan 20% van de totale lading verschillen.
            //

            //for (int i = 0; i < Columns.Length; i++)
            //{
            //    if (Columns[i].TryAdd(container)) return true;
            //}
            //return false;

            // get total weight
            int currentTotalWeightLeft = GetTotalWeightLeft();
            int currentTotalWeightRight = GetTotalWeightRight();
            double currentTotalWeight = GetTotalWeight();

            double leftTotalRatio = totalLeft / (totalLeft + totalRight);
            double rightTotalRatio = totalRight / (totalLeft + totalRight);

            // gewicht aan een kant (left)
            // mag niet meer

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
            for (int i = 0; i < Columns.Length / 2; i++)
            {
                if (Columns[i].TryAdd(container)) return true;
            }
            return false;
        }

        private bool TryAddRight(Container container)
        {
            for (int i = 0; i < Columns.Length / 2; i++)
            {
                if (Columns[^(i + 1)].TryAdd(container)) return true;
            }
            return false;
        }

        public int GetTotalWeight()
        {
            return Columns.Sum(c => c.GetTotalWeight());
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
