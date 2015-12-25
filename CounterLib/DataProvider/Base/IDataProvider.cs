namespace CounterLib.DataProvider.Base
{
    public interface IDataProvider<in TSourceId, out TData>
    {
        void Init(TSourceId sourceId);
        TData GetNext();
        bool EndOfDataStream { get; }

    }
}
