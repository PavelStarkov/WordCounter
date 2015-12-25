namespace CounterLib.DataPublisher.Base
{
    public interface IOutputDevice
    {
        void Write(object obj);
        void Write(string str);
    }
}
