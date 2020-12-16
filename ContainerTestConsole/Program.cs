using ContainerVervoer.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("(this is one row on the ship)");
            Console.WriteLine("stacked, values, here");
            Console.WriteLine("each, one, is, a, column");
            Console.WriteLine("bottom, to, top");
            Console.WriteLine();


            List<Container> containers = new List<Container>()
            {
                new Container(0, ContainerType.Normal),
                new Container(0, ContainerType.Normal),
                new Container(0, ContainerType.Valuable),
                new Container(0, ContainerType.Valuable),
                new Container(0, ContainerType.Coolable),
                new Container(0, ContainerType.Normal),
                new Container(0, ContainerType.ValuableAndCoolable),
                new Container(0, ContainerType.Normal),
            };

            CargoShip ship = new CargoShip(2, 2, containers.Sum(c => c.Weight));

            ContainerRow[] rows = ship.Divide(containers);

            Console.WriteLine("-----------");
            foreach (ContainerRow row in rows)
            {
                Console.Write(row.ToString());
                Console.WriteLine("-----------");
            }
        }
    }
}
