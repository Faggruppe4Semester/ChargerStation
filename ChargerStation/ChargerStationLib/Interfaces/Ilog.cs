namespace ChargerStation.Interfaces
{
    public interface Ilog
    {
        void LogDoorLocked(int LockID);
        void LogDoorUnlocked(int LockID);
    }
}