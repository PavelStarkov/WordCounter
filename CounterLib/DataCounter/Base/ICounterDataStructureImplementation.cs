namespace CounterLib.DataCounter.Base
{
    public interface ICounterDataStructureImplementation<in TInputData, out TOutputData> : IGetCounterResult<TOutputData>
    {
        void AddData(TInputData data, int amount);
    }
}
