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
        private IChargerControl _chargerControl;
        private IRfIdReader _idReader;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _chargerControl = Substitute.For<IChargerControl>();
            _idReader = Substitute.For<IRfIdReader>();

            _uut = new StationControl(_door, _chargerControl, _idReader);

        }
        [Test]
        public void Test1()
        {
            Assert.True(true);
        }
    }
}