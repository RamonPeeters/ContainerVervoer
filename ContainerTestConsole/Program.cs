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
                new Container(346, ContainerType.Normal),
                new Container(873, ContainerType.Normal),
                new Container(182, ContainerType.Normal),
                new Container(784, ContainerType.Normal),
                new Container(505, ContainerType.Normal),
                new Container(133, ContainerType.Normal),
                new Container(126, ContainerType.Coolable),
                new Container(173, ContainerType.Coolable),
                new Container(284, ContainerType.Valuable),
                new Container(183, ContainerType.Valuable),
                new Container(177, ContainerType.Valuable),
                new Container(185, ContainerType.Valuable),
                new Container(137, ContainerType.Valuable),
                new Container(190, ContainerType.ValuableAndCoolable),
                new Container(168, ContainerType.Valuable),
                new Container(302, ContainerType.ValuableAndCoolable),
                new Container(547, ContainerType.Normal),
                new Container(284, ContainerType.Normal),
                new Container(273, ContainerType.Normal),
                new Container(941, ContainerType.Normal),
                new Container(725, ContainerType.Normal),
                new Container(825, ContainerType.Normal),
                new Container(477, ContainerType.Coolable),
                new Container(168, ContainerType.Coolable),
                new Container(294, ContainerType.Valuable),
                new Container(624, ContainerType.Valuable),
                new Container(157, ContainerType.Valuable),
                new Container(345, ContainerType.ValuableAndCoolable),
            };

            CargoShip ship = new CargoShip(5, 7, containers.Sum(c => c.Weight));

            try
            {
                ContainerRow[] rows = ship.Divide(containers);

                for (int i = 0; i < rows.Length; i++)
                {
                    Console.WriteLine($"----------- {i}:");
                    Console.Write(rows[i].ToString());
                }
                Console.WriteLine("-----------");

                Console.WriteLine(ship.GetTotalWeightString());
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
