namespace ChargerStation.Interfaces
{
    public interface IStreamWriter
    {
        void Write(string text);
        void WriteLine(string Line);
        void Close();

    }
}