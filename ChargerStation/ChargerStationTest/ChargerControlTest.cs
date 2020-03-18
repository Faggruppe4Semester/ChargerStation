using System;
using ChargerStation;
using NSubstitute;
using NUnit.Framework;
using UsbSimulator;


namespace ChargerStationTest
{
    [TestFixture]
    public class ChargerControlTest
    {
        private ChargerControl _uut;
        private IUsbCharger _uc;
        private IDisplay _dp;

        [SetUp]
        public void SetUp()
        {
            _uc = new FakeUsbCharger();
            _dp = new FakeDisplay();
            _uut = new ChargerControl(_uc, _dp);

        }


        [Test]
        public void CurrentIs0_NothingIsWrittenToDisplay()
        {
            ((FakeUsbCharger)_uc).CurrentValue = 0;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo(null));
        }

        [Test]
        public void CurrentIsLow_WritePhoneIsFull()
        {
            ((FakeUsbCharger)_uc).CurrentValue = 3;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo("Phone is fully charged."));
        }

        [Test]
        public void CurrentIs250_WritePhoneIsCharging()
        {
            ((FakeUsbCharger)_uc).CurrentValue = 250;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo("Charging is in progress."));
        }

        [Test]
        public void CurrentIsOver500_WritePhoneError()
        {
            ((FakeUsbCharger)_uc).CurrentValue = 700;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo("Error. Please disconnect your phone immediately."));
        }


    }

    public class FakeDisplay : IDisplay
    {
        public string content;
        public void DisplayMessage(string message)
        {
            content = message;
        }
    }

    public class FakeUsbCharger : IUsbCharger
    {
        public double CurrentValue { set; get; }

        public bool Connected { set; get; }

        public event EventHandler<CurrentEventArgs> CurrentValueEvent;

        public void StartCharge()
        {
        }

        public void StopCharge()
        {
        }

        public void OnNewCurrent()
        {
            CurrentValueEvent?.Invoke(this, new CurrentEventArgs() { Current = this.CurrentValue });
        }
    }
}





