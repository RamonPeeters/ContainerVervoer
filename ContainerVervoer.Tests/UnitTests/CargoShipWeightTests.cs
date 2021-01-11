using ContainerVervoer.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ContainerVervoer.Tests.UnitTests
{
    [TestClass]
    public class CargoShipWeightTests
    {
        [TestMethod]
        public void CargoShipContents_NotEnoughWeight()
        {
            // Arrange
            CargoShip ship = new CargoShip(1, 1, 100);
            List<Container> containers = new List<Container>() { new Container(0, ContainerType.Normal) };

            // Act, Assert
            Assert.ThrowsException<Exception>(() => ship.Divide(containers));
        }

        [TestMethod]
        public void CargoShipContents_TooMuchWeight()
        {
            // Arrange
            CargoShip ship = new CargoShip(1, 1, 100);
            List<Container> containers = new List<Container>() { new Container(1000, ContainerType.Normal) };

            // Act, Assert
            Assert.ThrowsException<Exception>(() => ship.Divide(containers));
        }

        [TestMethod]
        public void CargoShipContents_UnbalancedWeight()
        {
            // Arrange
            CargoShip ship = new CargoShip(1, 1, 200);
            List<Container> containers = new List<Container>() { new Container(75, ContainerType.Normal), new Container(25, ContainerType.Normal) };

            // Act, Assert
            Assert.ThrowsException<Exception>(() => ship.Divide(containers));
        }
    }
}
