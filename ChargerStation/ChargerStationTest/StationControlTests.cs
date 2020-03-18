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
        public void ZeroTest_StateAvailable()
        {
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Available));
        }
      
        [Test]
        public void IDDetected_ChargingTrue_StateLocked()
        {
            _chargerControl.IsConnected().Returns(true);
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs {Id = 123});
            //_idReader.Detect(123);
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
        }

        [Test]
        public void IDDetected_ChargingFalse_StateAvailable()
        {
            _chargerControl.IsConnected().Returns(false);
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs{Id = 123});
            Assert.That(_uut._state,Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void IDDetected_CorrectId_StateAvailable()
        {
            _chargerControl.IsConnected().Returns(true);
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 }); //Lås skab
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 }); //Forsøg at låse op
            Assert.That(_uut._state,Is.EqualTo(StationControl.LadeskabState.Available));
        }

        [Test]
        public void IDDetected_IncorrectId_StateLocked()
        {
            _chargerControl.IsConnected().Returns(true);
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 }); //Lås skab
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 321 }); //Forsøg at låse op
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.Locked));
        }

        [Test]
        public void DoorOpened_StateDoorOpened()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs{State = DoorState.Open});
            Assert.That(_uut._state,Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }

        [Test]
        public void DoorOpened_IDreadIgnored_StateDoorOpen()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs{Id = 123});
            Assert.That(_uut._state, Is.EqualTo(StationControl.LadeskabState.DoorOpen));
        }

        [Test]
        public void DoorClosed_StateAvailable()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });
            Assert.That(_uut._state,Is.EqualTo(StationControl.LadeskabState.Available));
        }
    }
}