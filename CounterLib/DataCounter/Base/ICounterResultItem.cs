namespace CounterLib.DataCounter.Base
{
    public interface ICounterResultItem<out TData>
    {
        TData Item { get; }
        int TotalAmount { get; }
    }
}
