namespace ChargerStation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IDoor door = new Door();
            IChargerControl c = new ChargerControl(new UsbCharger());
            IRfIdReader rfid = new RfIdReader();
            StationControl sc = new StationControl(door, c, rfid);
            //TODO: Eventhandleing in chargercontol
            //TODO: Display needs updating
            //TODO: TESTING TESTING TESTING
        }
    }
}