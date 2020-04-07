using System;
using ChargerStation;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using NUnit.Framework.Internal.Execution;


namespace ChargerStationTest
{
    [TestFixture]
    public class StationControlTests
    {
        private StationControl _uut;
        private IDoor _door;
        private IChargerControl _chargerControl;
        private IRfIdReader _idReader;
        private Ilog _log;
        private IDisplay _display;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _chargerControl = Substitute.For<IChargerControl>();
            _idReader = Substitute.For<IRfIdReader>();
            _log = Substitute.For<Ilog>();
            _display = Substitute.For<IDisplay>();

            _uut = new StationControl(_door, _chargerControl, _idReader, _log, _display);

        }

        [Test]
        public void Unlocked_IdDetected_logCalled()
        {
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs {State = DoorState.Open});
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs {State = DoorState.Closed});

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs {Id = 123});

            _log.Received(1).LogDoorLocked(123);
        }

        [Test]
        public void Unlocked_IdDetected_DisplayWritten()
        {
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            _display.Received(1).DisplayMessage("Ladeskab optaget");
        }

        [Test]
        public void Unlocked_IdDetected_ChargeStarted()
        {
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            _chargerControl.Received(1).StartCharge();
        }

        //Ikke connected besked

        //Open: besked
        //Close: besked

        //Låst: forkertID besked
        //Låst: korrekt id besked
    }
}