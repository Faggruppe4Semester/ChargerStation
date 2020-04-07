using ChargerStation;
using ChargerStation.Interfaces;
using NUnit.Framework;

namespace ChargerStationTest
{
    [TestFixture]
    public class RFIDReaderTests
    {
        private IRfIdReader _idReader;
        private RfIdEventArgs _idEventArgs;

        [SetUp]
        public void SetUp()
        {
            _idReader = new RfIdReader();
            _idReader.RfIdDetectedEvent += (obj, args) =>
            {
                _idEventArgs = args;

            };
        }

        [Test]
        public void IdDetected_EventRaised()
        {
            _idReader.Detect(123);
            Assert.That(_idEventArgs.Id,Is.EqualTo(123));
        }
    }
}