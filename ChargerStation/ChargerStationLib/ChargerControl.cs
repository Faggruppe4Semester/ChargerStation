using UsbSimulator;

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
        private IDisplay _display;
        public ChargerControl(IUsbCharger usb, IDisplay display)
        {
            _usbCharger = usb;
            _usbCharger.CurrentValueEvent += HandleCurrentEvent;
            _display = display;
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

        void HandleCurrentEvent(object source, CurrentEventArgs e)
        {
            if(e.Current == 0)
            {
                //Der er ingen forbindelse til en telefon, eller ladning er ikke startet. Displayet viser ikke noget om ladning
            }
            else if(0 < e.Current && e.Current <= 5)
            {
                //Opladningen er tilendebragt, og USB ladningen kan frakobles. Displayet viser, at telefonen er fuldt opladet.
                _display.DisplayMessage("Phone is fully charged.");
            }
            else if(5 < e.Current && e.Current <= 500)
            {
                //Opladningen foregår normalt. Displayet viser, at ladning foregår
                _display.DisplayMessage("Charging is in progress.");
            }
            else if(e.Current > 500)
            {
                //Der er noget galt, fx en kortslutning. USB ladningen skal straks frakobles. Displayet viser en fejlmeddelelse
                _display.DisplayMessage("Error. Please disconnect your phone immediately.");
            }
        }
    }
}