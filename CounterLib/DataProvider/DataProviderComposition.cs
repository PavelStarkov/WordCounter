using System;
using CounterLib.DataProvider.Base;

namespace CounterLib.DataProvider
{
    public class DataProviderComposition<TSourceOfSourcesId, TSourceId, TData> : IDataProvider<TSourceOfSourcesId, TData>
    {
        private readonly IDataProvider<TSourceOfSourcesId, TSourceId> _sourcesDataProvider;
        private readonly IDataProvider<TSourceId, TData> _dataProvider; 
        public DataProviderComposition(IDataProvider<TSourceOfSourcesId, TSourceId> sourcesDataProvider, IDataProvider<TSourceId, TData> dataProvider)
        {
            _sourcesDataProvider = sourcesDataProvider;
            _dataProvider = dataProvider;
        }

        public void Init(TSourceOfSourcesId sourceId)
        {
            _sourcesDataProvider.Init(sourceId);
        }
        public bool EndOfDataStream => _sourcesDataProvider.EndOfDataStream && _dataProvider.EndOfDataStream;

        public TData GetNext()
        {
            while (!EndOfDataStream)
            {
                if (!_dataProvider.EndOfDataStream)
                {
                    return _dataProvider.GetNext();
                }

                if (!_sourcesDataProvider.EndOfDataStream)
                {
                    _dataProvider.Init(_sourcesDataProvider.GetNext());
                }
            }
            throw new IndexOutOfRangeException();
        }
    }
}
