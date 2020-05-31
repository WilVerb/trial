using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;

namespace UnitTestProject1
{
    [TestClass]
    public class Class1Tests
    {
        [TestMethod]
        public void TestReturn10()
        {
            //Arrange
            int expected_res = 10;
            Class1 class1 = new Class1();

            //Act
            int actual_res = class1.Return10();

            //Assert
            Assert.AreEqual(expected_res, actual_res);
        }
    }
}
