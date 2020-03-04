namespace ChargerStation
{
    public interface IDoor
    {
        void LockDoor();
        void UnlockDoor();
    }
    
    public class Door : IDoor
    {
        public void LockDoor()
        {
            throw new System.NotImplementedException();
        }

        public void UnlockDoor()
        {
            throw new System.NotImplementedException();
        }
    }
}