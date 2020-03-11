namespace ChargerStation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IDoor door = new Door();
            IUsbCharger usb = new UsbCharger();
            IRfIdReader rfid = new RfIdReader();
            StationControl sc = new StationControl(door, usb, rfid);
        }
    }
}