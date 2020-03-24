using System;
using ChargerStation;
using NSubstitute;
using NUnit.Framework;


namespace ChargerStationTest
{
    [TestFixture]
    public class DoorTest
    {
        private IDoor _door;
        private DoorStateChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void SetUp()
        {
            _door = new Door();
            _door.DoorStateChangedEvent += (obj, args) =>
                {
                    _receivedEventArgs = args;
                };

        }

        [Test]
        public void NoCalls_StateIsClosed()
        {
            Assert.That(_door.State, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void DoorClosed_StateIsClosed()
        {
            _door.State = DoorState.Open;
            _door.DoorClosed();
            Assert.That(_door.State,Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void DoorOpen_StateIsOpen()
        {
            _door.State = DoorState.Closed;
            _door.DoorOpen();
            Assert.That(_door.State, Is.EqualTo(DoorState.Open));
        }

        [Test]
        public void ManyClosed_StateIsClosed()
        {
            _door.State = DoorState.Open;
            _door.DoorOpen();
            _door.DoorClosed();
            _door.DoorOpen();
            _door.DoorClosed();
            Assert.That(_door.State, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void ManyOpen_StateIsOpen()
        {
            _door.State = DoorState.Closed;
            _door.DoorClosed();
            _door.DoorOpen();
            _door.DoorClosed();
            _door.DoorOpen();
            Assert.That(_door.State, Is.EqualTo(DoorState.Open));
        }





        [Test]
        public void DoorClosed_EventStateIsClosed()
        {
            _door.DoorClosed();
            Assert.That(_receivedEventArgs.State, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void DoorOpen_EventStateIsOpen()
        {
            _door.DoorOpen();
            Assert.That(_receivedEventArgs.State, Is.EqualTo(DoorState.Open));
        }
    }
}