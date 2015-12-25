using System;
using System.IO;
using CounterLib.DataProvider.Base;

namespace CounterLib.DataProvider
{
    public class TextProvider : IDataProvider<string, string>
    {
        private string _sourceId;

        public TextProvider(string sourceId)
        {
            Init(sourceId);
        }

        public void Init(string sourceId)
        {
            _sourceId = sourceId;
        }

        public bool EndOfDataStream => string.IsNullOrEmpty(_sourceId);

        public string GetNext()
        {
            if (EndOfDataStream) throw new IndexOutOfRangeException();
            var text = File.Exists(_sourceId) ? File.ReadAllText(_sourceId) : _sourceId;
            _sourceId = null;
            return text;
        }
    }
}
