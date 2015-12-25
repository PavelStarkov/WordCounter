using CounterLib.DataCounter.Base;

namespace CounterLib.DataCounter
{
    public class CounterResultItem<TData> : ICounterResultItem<TData>
    {
        public CounterResultItem(TData item, int totalAmount)
        {
            Item = item;
            TotalAmount = totalAmount;
        }
        public TData Item { get; }
        public int TotalAmount { get; }
    }
}
