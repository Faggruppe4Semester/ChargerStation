namespace ChargerStation
{
    interface IChargerControl
    {
        bool IsConnected();
        void StartCharge();
        void StopCharge();
    }
    
    public class ChargerControl : IChargerControl
    {
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