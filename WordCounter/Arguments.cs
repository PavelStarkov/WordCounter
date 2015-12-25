using System;
using CounterLib.Workflow.Base;
using WordCounter.Properties;
using NDesk.Options;

namespace WordCounter
{
    public class Arguments : IWorkflowSetting
    {
        public string Input { get; private set; }
        public bool IsValid { get; }
        public string DataStructure { get; private set; }
        public string SortingAlgorithm { get; private set; }
        public int WorkersCount { get; private set; }
        public bool ShowHelp { get; private set; }

        public int DefaultMultiThreadedTextChunkSize { get; private set; }


        public Arguments(string[] args)
        {
            Init(args);

            if (!ShowHelp && (args.Length == 1) && string.IsNullOrEmpty(Input)) Input = args[0];

            IsValid = !string.IsNullOrEmpty(Input);

            if (ShowHelp || !IsValid) Console.WriteLine(Resources.HelpDescription);
        }

        private void Init(string[] args)
        {
            var p = new OptionSet() {

                {  Resources.InputKey, Resources.InputDescription, v => Input = v },
                {  Resources.HelpKey, Resources.HelpDescription,  v => ShowHelp = !string.IsNullOrEmpty(v) },
                {  Resources.DataStructureKey, Resources.DataStructureDescription,  v => DataStructure = v },
                {  Resources.SortingAlgorithmKey, Resources.SortingAlgorithmDescription,  v => SortingAlgorithm = v },
                {  Resources.WorkersCountKey, Resources.WorkersCountDescription,  v => WorkersCount = int.Parse(v)  },
                {  Resources.ChunkSizeKey, Resources.ChunkSizeDescription,  v => DefaultMultiThreadedTextChunkSize = int.Parse(v)  },

            };

            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(Resources.ArgsParserErrorMessage);
                throw;
            }
        }
    }
}
