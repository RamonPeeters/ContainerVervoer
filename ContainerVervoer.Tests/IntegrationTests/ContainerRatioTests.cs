using ContainerVervoer.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ContainerVervoer.Tests.IntegrationTests
{
    [TestClass]
    public class ContainerRatioTests
    {
        [TestMethod]
        public void SmallSetOfContainers_ShouldBeDividedWithinLimits()
        {
            // Arrange
            CargoShip ship = new CargoShip(2, 2, 120);
            List<Container> containers = new List<Container>()
            {
                new Container(30, ContainerType.Normal),
                new Container(30, ContainerType.Normal),
                new Container(30, ContainerType.Normal),
                new Container(30, ContainerType.Normal)
            };

            // Act
            ship.Divide(containers);
            double leftWeightRatio = ship.GetLeftWeightRatio();

            // Assert
            Assert.AreEqual(0.5d, leftWeightRatio, 0.2d);
        }

        [TestMethod]
        public void LargeSetOfContainers_ShouldBeDividedWithinLimits()
        {
            // Arrange
            CargoShip ship = new CargoShip(4, 3, 4000);
            List<Container> containers = new List<Container>()
            {
                new Container(225, ContainerType.Normal),
                new Container(50, ContainerType.Normal),
                new Container(150, ContainerType.Coolable),
                new Container(100, ContainerType.Normal),
                new Container(100, ContainerType.Normal),
                new Container(175, ContainerType.Valuable),
                new Container(150, ContainerType.Normal),
                new Container(100, ContainerType.ValuableAndCoolable),
                new Container(100, ContainerType.Normal),
                new Container(100, ContainerType.Valuable),
                new Container(100, ContainerType.Normal),
                new Container(375, ContainerType.Normal),
                new Container(50, ContainerType.Valuable),
                new Container(100, ContainerType.Coolable),
                new Container(150, ContainerType.ValuableAndCoolable),
                new Container(100, ContainerType.Normal),
                new Container(250, ContainerType.Normal),
                new Container(300, ContainerType.Valuable),
                new Container(100, ContainerType.Normal),
                new Container(275, ContainerType.Coolable)
            };

            // Act
            ship.Divide(containers);
            double leftWeightRatio = ship.GetLeftWeightRatio();

            // Assert
            Assert.AreEqual(0.5d, leftWeightRatio, 0.2d);
        }
    }
}
