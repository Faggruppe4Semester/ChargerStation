using System;
using ChargerStation;
using ChargerStation.Interfaces;
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

        /* EP's
         *  Ep1 [ Ladestrøm <= -1 mA]
         *  Ep2 [ 0 mA]
         *  Ep3 [1 mA <= Ladestrøm <= 5 ]
         *  Ep4 [ 5 < Ladestrøm <= 500 ]
         *  Ep5 [ 500 < Ladestrøm ]
         */
        
        [TestCase(-1)]
        [TestCase(-500)]
        public void BVA_EP1_CurrentIsBelowZero_NothingIsWrittenToDisplay(int a)
        {
            ((FakeUsbCharger)_uc).CurrentValue = a;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo(null));
        }

        [TestCase(0)]
        public void BVA_EP2_CurrentIsZero_NothingIsWrittenToDisplay(int a)
        {
            ((FakeUsbCharger)_uc).CurrentValue = a;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo(null));
        }
        
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void BVA_EP3_CurrentIsAcceptableRangeForFullCharge_WritePhoneIsFullToDisplay(int a)
        {
            ((FakeUsbCharger)_uc).CurrentValue = a;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo("Phone is fully charged."));
        }
        
        [TestCase(6)]
        [TestCase(250)]
        [TestCase(500)]
        public void BVA_EP4_CurrentIsAcceptableRangeForChargeInProgress_WritePhoneIsChargingToDisplay(int a)
        {
            ((FakeUsbCharger)_uc).CurrentValue = a;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo("Charging is in progress."));
        }
        
        [TestCase(501)]
        [TestCase(1000)]
        public void BVA_EP5_CurrentIsAbove500mA_WriteErrorToDisplay(int a)
        {
            ((FakeUsbCharger)_uc).CurrentValue = a;
            ((FakeUsbCharger)_uc).OnNewCurrent();
            Assert.That(((FakeDisplay)_dp).content, Is.EqualTo("Error. Please disconnect your phone immediately."));
        }
        
        [Test]
        public void StartCharge_ConnectedIsTrue()
        {
            _uut.StartCharge();
            Assert.That(((FakeUsbCharger)_uc).Connected, Is.EqualTo(true));
        }

        [Test]
        public void StopCharge_ConnectedIsFalse()
        {
            _uut.StopCharge();
            Assert.That(((FakeUsbCharger)_uc).Connected, Is.EqualTo(false));
        }

        [Test]
        public void ConnectedIsFalse_IsConnectedIsFalse()
        {
            ((FakeUsbCharger)_uc).Connected = false;
            Assert.That(_uut.IsConnected, Is.EqualTo(false));
        }

        [Test]
        public void ConnectedIsTrue_IsConnectedIsTrue()
        {
            ((FakeUsbCharger)_uc).Connected = true;
            Assert.That(_uut.IsConnected, Is.EqualTo(true));
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
            Connected = true;
        }

        public void StopCharge()
        {
            Connected = false;
        }

        public void OnNewCurrent()
        {
            CurrentValueEvent?.Invoke(this, new CurrentEventArgs() { Current = this.CurrentValue });
        }
    }
}





