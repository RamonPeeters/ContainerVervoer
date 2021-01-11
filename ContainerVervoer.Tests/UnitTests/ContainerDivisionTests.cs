using ContainerVervoer.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ContainerVervoer.Tests.UnitTests
{
    [TestClass]
    public class ContainerDivisionTests
    {
        [TestMethod]
        public void ValuableContainer_ShouldBeInBackOrFront()
        {
            // Arrange
            CargoShip ship = new CargoShip(3, 1, 100);
            Container valuableContainer = new Container(30, ContainerType.Valuable);
            Container normalContainer = new Container(30, ContainerType.Normal);
            List<Container> containers = new List<Container>() { normalContainer, valuableContainer, valuableContainer };

            // Act
            ContainerRow[] rows = ship.Divide(containers);

            // Assert
            Assert.IsTrue(rows[0][0][0].Type == ContainerType.Valuable && rows[2][0][0].Type == ContainerType.Valuable);
        }

        [TestMethod]
        public void ValuableContainer_ShouldThrow_BecauseNotEnoughSpace()
        {
            // Arrange
            CargoShip ship = new CargoShip(3, 1, 100);
            Container valuableContainer = new Container(30, ContainerType.Valuable);
            List<Container> containers = new List<Container>() { valuableContainer, valuableContainer, valuableContainer };

            // Act, Assert
            Assert.ThrowsException<Exception>(() => ship.Divide(containers));
        }

        [TestMethod]
        public void CoolableContainer_ShouldBeInFront()
        {
            // Arrange
            CargoShip ship = new CargoShip(3, 1, 100);
            Container coolableContainer = new Container(30, ContainerType.Coolable);
            Container normalContainer = new Container(30, ContainerType.Normal);
            List<Container> containers = new List<Container>() { normalContainer, normalContainer, coolableContainer };

            // Act
            ContainerRow[] rows = ship.Divide(containers);

            // Assert
            Assert.IsTrue(rows[0][0][0].Type == ContainerType.Coolable);
        }

        [TestMethod]
        public void ValuableContainer_ShouldDivide_BecauseCoolableMayBeStacked()
        {
            // Arrange
            CargoShip ship = new CargoShip(3, 1, 100);
            Container coolableContainer = new Container(30, ContainerType.Coolable);
            List<Container> containers = new List<Container>() { coolableContainer, coolableContainer, coolableContainer };

            // Act
            ContainerRow[] rows = ship.Divide(containers);

            // Assert
            Assert.IsTrue(rows[0].GetTotalContainers() == 3);
        }
    }
}
