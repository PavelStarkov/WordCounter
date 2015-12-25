namespace CounterLib.DataSorting.Base
{
    public interface ISortSettings<out TKey>
    {
        TKey MaxKey { get; }
    }
}
