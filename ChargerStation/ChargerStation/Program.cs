namespace ChargerStation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IDoor d = new Door();
            StationControl sc = new StationControl(d);
        }
    }
}