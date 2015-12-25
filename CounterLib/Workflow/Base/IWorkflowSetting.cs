namespace CounterLib.Workflow.Base
{
    public interface IWorkflowSetting
    {
        string Input { get; }
        bool IsValid { get;  }
        string DataStructure { get; }
        string SortingAlgorithm { get;  }
        int WorkersCount { get; }
        int DefaultMultiThreadedTextChunkSize { get; }
    }
}
