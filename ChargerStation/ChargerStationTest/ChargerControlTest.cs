using System;
using ChargerStation;
using NSubstitute;
using NUnit.Framework;


namespace ChargerStationTest
{
    [TestFixture]
    public class ChargerControlTest
    {


        [SetUp]
        public void SetUp()
        {


        }
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }
    }


    public class FakeDisplay : IDisplay
    {
        string content;
        public void DisplayMessage(string message)
        {
            content = message;
        }
    }
}