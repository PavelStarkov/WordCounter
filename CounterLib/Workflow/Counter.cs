using CounterLib.DataCounter.Base;
using CounterLib.DataProvider.Base;

namespace CounterLib.Workflow
{
    public class Counter<TSourceId, TInputData, TOutputData> : IGetCounterResult<TOutputData>
    {
        private readonly IDataProvider<TSourceId, TInputData> _dataProvider;
        private readonly ICounterDataStructureImplementation<TInputData, TOutputData> _counterDataStructure;

        public Counter(IDataProvider<TSourceId, TInputData> dataProvider, ICounterDataStructureImplementation<TInputData, TOutputData> counterDataStructure)
        {
            _dataProvider = dataProvider;
            _counterDataStructure = counterDataStructure;
        }

        public ICounterResult<TOutputData> GetResult()
        {
            while (!_dataProvider.EndOfDataStream)
            {
                _counterDataStructure.AddData(_dataProvider.GetNext(), 1);
            }

            return _counterDataStructure.GetResult();
        }
    }
}
