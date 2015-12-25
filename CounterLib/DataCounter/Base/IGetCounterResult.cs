namespace CounterLib.DataCounter.Base
{
    public interface IGetCounterResult<out TData>
    {
        ICounterResult<TData> GetResult();
    }
}
