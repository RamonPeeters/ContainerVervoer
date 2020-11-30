using ContainerVervoer.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ContainerVervoer.Tests
{
    [TestClass]
    public class CargoShipWeightTests
    {
        [TestMethod]
        public void CargoShipContents_NotEnoughWeight()
        {
            // Arrange
            CargoShip ship = new CargoShip(1, 1, 10000);
            List<CargoObject> containers = new List<CargoObject>() { new Container(0, ContainerType.Normal) };

            // Act, Assert
            Assert.ThrowsException<Exception>(() => ship.Divide(containers));
        }

        [TestMethod]
        public void CargoShipContents_TooMuchWeight()
        {
            // Arrange
            CargoShip ship = new CargoShip(1, 1, 10000);
            List<CargoObject> containers = new List<CargoObject>() { new Container(100000, ContainerType.Normal) };

            // Act, Assert
            Assert.ThrowsException<Exception>(() => ship.Divide(containers));
        }
    }
}
