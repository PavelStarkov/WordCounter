namespace CounterLib.DataPublisher.Base
{
    public interface IDataPublisher<in TData>
    {
        void Publish(TData data);
    }
}
