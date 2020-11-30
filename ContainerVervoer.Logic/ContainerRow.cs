namespace ContainerVervoer.Logic
{
    public class ContainerRow
    {
        // .....
        // .....
        // .....
        // .....
        // XXXXX

        private readonly ContainerColumn[] Columns;

        public ContainerRow(int columns)
        {
            // Initialise and populate
            Columns = new ContainerColumn[columns];
            for (int i = 0; i < columns; i++) Columns[i] = new ContainerColumn();
        }

        public bool TryAdd(CargoObject container)
        {
            // Fill from left, then right
            for (int i = 0; i < Columns.Length / 2; i++)
            {
                if (Columns[i].TryAdd(container)) return true;
                else if (Columns[^(i + 1)].TryAdd(container)) return true;
            }

            if (Columns.Length % 2 == 0)
            {
                // Even width
                return false;
            } else
            {
                // Odd with, scan centre
                return Columns[Columns.Length / 2].TryAdd(container);
            }
        }
    }
}
