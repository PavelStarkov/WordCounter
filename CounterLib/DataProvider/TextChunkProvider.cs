using System;
using CounterLib.DataProvider.Base;

namespace CounterLib.DataProvider
{
    public class TextChunkProvider : IDataProvider<string, string>
    {
        private readonly int _chunkSize;
        private int _lastPos;
        private string _text;
        private int _pos;

        public TextChunkProvider(int chunkSize)
        {
            _chunkSize = chunkSize;
        }

        public void Init(string sourceId)
        {
            _text = sourceId;
            _pos = 0;
            if (!string.IsNullOrEmpty(_text)) _lastPos = _text.Length - 1;
        }

        public bool EndOfDataStream => string.IsNullOrEmpty(_text) || _pos > _lastPos;

        public string GetNext()
        {
            if (EndOfDataStream) throw new IndexOutOfRangeException();

            var nextChunkSize = GetNextChunkSize();
            var strChunk = _text.Substring(_pos, nextChunkSize);
            _pos += nextChunkSize;
            return strChunk;
        }

        private int GetNextChunkSize()
        {
            var nextChunkSize = _chunkSize;
            while (_pos + nextChunkSize <= _lastPos && char.IsLetter(_text[_pos + nextChunkSize]))
                nextChunkSize++;

            if (_pos + nextChunkSize > _lastPos) nextChunkSize = _text.Length - _pos;

            return nextChunkSize;
        }
    }
}
