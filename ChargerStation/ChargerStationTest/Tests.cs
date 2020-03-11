using System;
using ChargerStation;
using NSubstitute;
using NUnit.Framework;


namespace ChargerStationTest
{
    [TestFixture]
    public class StationControlTests
    {
        private StationControl _uut;
        private IDoor _door;
        private IUsbCharger _usbCharger;
        private IRfIdReader _idReader;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
        }
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}