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
            return false;
        }

        public void StartCharge()
        {
            
        }

        public void StopCharge()
        {
            
        }
    }
}