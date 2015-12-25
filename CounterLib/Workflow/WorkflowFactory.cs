using System;
using System.Collections.Generic;
using CounterLib.DataCounter;
using CounterLib.DataCounter.Base;
using CounterLib.DataProvider;
using CounterLib.DataProvider.Base;
using CounterLib.DataPublisher;
using CounterLib.DataSorting;
using CounterLib.DataSorting.Base;
using CounterLib.Workflow.Base;

namespace CounterLib.Workflow
{
    public class WorkflowFactory
    {
        public IWorkflow GetWorkflow(IWorkflowSetting settings)
        {
            //DataPublisher
            var outputDevice = new ConsoleOutputDevice();
            var dataPublisher = new CounterResultPublisher<string>(outputDevice);

            //DataCounter
            IGetCounterResult<string> counterResult;
            if (settings.WorkersCount > 1)
            {
                counterResult = new MultiThreadedCounter<string, string, string>(
                    GetMultiThreadedDataProvider(settings.Input, settings.DefaultMultiThreadedTextChunkSize), 
                    GetWorkerDataProvider,
                    (i) => GetDataStructure(settings.DataStructure, (i==0) ? settings.SortingAlgorithm : null), 
                    settings.WorkersCount);
            }
            else
            {
                counterResult = new Counter<string, string, string>(
                    GetDataProvider(settings.Input), 
                    GetDataStructure(settings.DataStructure, settings.SortingAlgorithm));
            }
            return new Workflow<string>(dataPublisher, counterResult);
        }

        private IDataProvider<string, string> GetMultiThreadedDataProvider(string sourceId, int chunkSize)
        {
            var cs = (chunkSize > 0) ? chunkSize : Constants.DefaultMultiThreadedTextChunkSize;
            return new DataProviderComposition<string, string, string>(new TextProvider(sourceId), new TextChunkProvider(cs));
        }

        private IDataProvider<string, string> GetDataProvider(string sourceId)
        {
            return new DataProviderComposition<string, string, string>(new TextProvider(sourceId), new WordProvider());
        }

        private IDataProvider<string, string> GetWorkerDataProvider(int workerNumber)
        {
            return new WordProvider();
        }

        private ISorter<int, ICounterResultItem<string>> GetSorter(string key)
        {
            return !string.IsNullOrEmpty(key) && _sorters.ContainsKey(key) ? _sorters[key]() : null;
        }

        private ICounterDataStructureImplementation<string, string> GetDataStructure(string key, string sorterKey)
        {
            return !string.IsNullOrEmpty(key) && _dataStructures.ContainsKey(key)
                ? _dataStructures[key](GetSorter(sorterKey))
                : _dataStructures[Constants.DefaultDataStructureKey](GetSorter(sorterKey));
        }

        private readonly Dictionary<string, Func<ISorter<int, ICounterResultItem<string>>>> _sorters = new Dictionary<string, Func<ISorter<int, ICounterResultItem<string>>>>()
        {
            {Constants.QuickSortKey,() => new QuickSort<int, ICounterResultItem<string>>() },
            {Constants.DistributionSortKey, () => new DistributionSort<ICounterResultItem<string>>() },
            {Constants.HybridSortKey,() => new HybridSort<ICounterResultItem<string>>(new DistributionSort<ICounterResultItem<string>>(), new QuickSort<int, ICounterResultItem<string>>()) }
        };

        private readonly Dictionary<string, Func<ISorter<int, ICounterResultItem<string>>, ICounterDataStructureImplementation<string, string>>> _dataStructures = new Dictionary<string, Func<ISorter<int, ICounterResultItem<string>>, ICounterDataStructureImplementation<string, string>>>()
        {
            {Constants.HashTableKey,(sorter) => new HashTableCounterDataStructureImplementation<string>(sorter) },
            {Constants.SortedListKey, (sorter) => new SortedListCounterImplementation<string>(sorter) },

        };
    }
}
