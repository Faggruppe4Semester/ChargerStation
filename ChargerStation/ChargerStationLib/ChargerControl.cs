﻿using UsbSimulator;

namespace ChargerStation
{
    public interface IChargerControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
    
    public class ChargerControl : IChargerControl
    {
        private IUsbCharger _usbCharger;
        public ChargerControl(IUsbCharger usb)
        {
            _usbCharger = usb;
        }
        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }
    }
}