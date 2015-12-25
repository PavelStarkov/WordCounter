using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using CounterLib.DataCounter.Base;
using CounterLib.DataProvider.Base;

namespace CounterLib.Workflow
{
    /// <summary>
    /// Allows to run the whole workflow in multithreaded way.  
    /// </summary>
    /// <typeparam name="TSourceId"></typeparam>
    /// <typeparam name="TWorkerSourceId"></typeparam>
    /// <typeparam name="TWorkerData"></typeparam>
    public class MultiThreadedCounter<TSourceId, TWorkerSourceId, TWorkerData> : IGetCounterResult<TWorkerData> where TWorkerSourceId : class
    {
        private readonly IDataProvider<TSourceId, TWorkerSourceId> _dataProvider;
        private readonly Func<int, IDataProvider<TWorkerSourceId, TWorkerData>> _workerDataProviderFactory;
        private readonly Func<int, ICounterDataStructureImplementation<TWorkerData, TWorkerData>> _workerCounterDataStructureFactory;
        private readonly int _workersCount;
        public MultiThreadedCounter(IDataProvider<TSourceId, TWorkerSourceId> dataProvider, Func<int, IDataProvider<TWorkerSourceId, TWorkerData>> workerDataProviderFactory, Func<int, ICounterDataStructureImplementation<TWorkerData, TWorkerData>> workerCounterDataStructureFactory, int workersCount)
        {
            _dataProvider = dataProvider;
            _workerDataProviderFactory = workerDataProviderFactory;
            _workerCounterDataStructureFactory = workerCounterDataStructureFactory;
            _workersCount = workersCount;
        }

        public ICounterResult<TWorkerData> GetResult()
        {
            if (_workersCount < 1) throw new ArgumentOutOfRangeException();
            var tasks = new Task[_workersCount];
            var workerCounterDataStructures = new ICounterDataStructureImplementation<TWorkerData, TWorkerData>[_workersCount];

            var workerInputData = new BlockingCollection<TWorkerSourceId>();

            Action<int> worker = (i) =>
            {
                var workerDataProvider =_workerDataProviderFactory(i);
                var workerCounterDataStructure = workerCounterDataStructures[i] = _workerCounterDataStructureFactory(i);
                foreach (TWorkerSourceId sourceId in workerInputData.GetConsumingEnumerable())
                {
                    if (sourceId == null) break;
                    workerDataProvider.Init(sourceId);
                    while (!workerDataProvider.EndOfDataStream)
                    {
                        workerCounterDataStructure.AddData(workerDataProvider.GetNext(), 1);
                    }
                }
                
            };

            for (var i = 0; i < _workersCount; i++)
            {
                var workerNumber = i;
                tasks[i] = Task.Factory.StartNew(() => worker(workerNumber));
            }

            while (!_dataProvider.EndOfDataStream)
            {
                workerInputData.Add(_dataProvider.GetNext());
            }

            //Adding nulls - simplified implementation for notifying workers to exit.
            for (var i = 0; i < _workersCount; i++)
                workerInputData.Add(null);

            Task.WaitAll(tasks);

            return MergeCounterDataStructures(workerCounterDataStructures);
        }

        private ICounterResult<TWorkerData> MergeCounterDataStructures(ICounterDataStructureImplementation<TWorkerData, TWorkerData>[] counterDataStructures)
        {
            if (counterDataStructures.Length < 1) throw new ArgumentException();
            var destinationCounter = counterDataStructures[0];
            for (var i = 1; i < counterDataStructures.Length; i++)
                foreach (var sourceItem in counterDataStructures[i].GetResult().Items)
                {
                    destinationCounter.AddData(sourceItem.Item, sourceItem.TotalAmount);
                }
            return destinationCounter.GetResult();
        }
    }
}
