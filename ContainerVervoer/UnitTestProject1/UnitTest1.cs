using ContainerVervoer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddContainersToList()
        {
            //arange
            int weight = 8000;
            int length = 6;
            int width = 6;
            int cooled = 30;
            int valued = 30;
            int normal = 30;

            //act
            Ship ship = new Ship(weight, length, width, cooled, valued, normal);

            //assert
            Assert.AreEqual(90, ship.Containers.Count);

        }

        [TestMethod]
        public void CheckWeightDifference()
        {
            //arange
            int weight = 8000;
            int length = 6;
            int width = 6;
            int cooled = 30;
            int valued = 30;
            int normal = 30;

            //act
            Ship ship = new Ship(weight, length, width, cooled, valued, normal);
            ship.OrderContainers();
            //assert
            Assert.AreEqual(true, ship.BalanceCheck20());

        }
    }
}
