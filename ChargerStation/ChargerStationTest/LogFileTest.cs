using System;
using ChargerStation;
using ChargerStation.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace ChargerStationTest
{
    [TestFixture]
    public class LogFileTest
    {
        private LogFile _uut;
        private IStreamWriter _streamWriter;

        [SetUp]
        public void SetUp()
        {
            _streamWriter = Substitute.For<IStreamWriter>();
            _uut = new LogFile(_streamWriter);
        }

        [Test]
        public void BehaviorTest_LockLog_WriterfunctionsCalled()
        {
            _uut.LogDoorLocked(123);
            //Tester den fulde log entry Datetime er i et format så tests ikke fejler med tid
            _streamWriter.Received(1).Write("Log entry: ");
            _streamWriter.Received(1).WriteLine($"{DateTime.Now.ToShortTimeString()} {DateTime.Now.ToLongDateString()}");
            _streamWriter.Received(1).WriteLine("Charging station locked using RFID: 123");
            _streamWriter.Received(1).WriteLine("-------------------------------------------------------");
            _streamWriter.Received(1).Close();
        }

        [Test]
        public void BehaviorTest_UnlockLog_LogEntryCorrect()
        {
            _uut.LogDoorUnlocked(123);
            _streamWriter.Received(1).Write("Log entry: ");
            _streamWriter.Received(1).WriteLine($"{DateTime.Now.ToShortTimeString()} {DateTime.Now.ToLongDateString()}");
            _streamWriter.Received(1).WriteLine("Charging station locked using RFID: 123");
            _streamWriter.Received(1).WriteLine("-------------------------------------------------------");
            _streamWriter.Received(1).Close();
        }
    }
}