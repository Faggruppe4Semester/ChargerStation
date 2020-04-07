namespace ChargerStation.Interfaces
{
    public interface IChargerControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
}