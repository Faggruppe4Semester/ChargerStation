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

        [Test]
        public void DoorOpen_IdDetected_Ignored()
        {
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs {Id = 123});

            _chargerControl.Received(0).StartCharge();
        }

        [Test]
        public void Unlocked_PhoneNotConnected_WritesErrorMessage()
        {
            _chargerControl.IsConnected().Returns(false);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs {Id = 123});

            _display.Received(1).DisplayMessage("Tilslutningsfejl");
        }

        [Test]
        public void Unlocked_DoorOpened_WritesConnectionMessage()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs {State = DoorState.Open});

            _display.Received(1).DisplayMessage("Tilslut telefon");
        }

        [Test]
        public void DoorOpen_DoorClosed_WriteClosedMessage()
        {
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _display.Received(1).DisplayMessage("Indlæs RFID");
        }

        [Test]
        public void Locked_WrongIdDetected_ErrorMessageDisplayed()
        {
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs {Id = 321});

            _display.Received(1).DisplayMessage("Forkert RFID");
        }

        [Test]
        public void Locked_CorrectIdDetected_RemovePhoneMessageDisplayed()
        {
            //Setup locked
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            //Unlock with correct ID
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            _display.Received(1).DisplayMessage("Fjern telefon");
        }

        [Test]
        public void Locked_CorrectIdDetected_UnlockLogged()
        {
            //Setup locked
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            //Unlock with correct ID
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            _log.Received(1).LogDoorUnlocked(123);
        }

        [Test]
        public void Locked_CorrectIdDetected_ChargeStopped()
        {
            //Setup locked
            _chargerControl.IsConnected().Returns(true);
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Open });
            _door.DoorStateChangedEvent += Raise.EventWith(new DoorStateChangedEventArgs { State = DoorState.Closed });

            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            //Unlock with correct ID
            _idReader.RfIdDetectedEvent += Raise.EventWith(new RfIdEventArgs { Id = 123 });

            _chargerControl.Received(1).StopCharge();
        }
    }
}