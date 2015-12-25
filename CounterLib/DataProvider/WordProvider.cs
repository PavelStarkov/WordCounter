using System;
using CounterLib.DataProvider.Base;

namespace CounterLib.DataProvider
{
    public class WordProvider : IDataProvider<string, string>
    {
        private int _pos;
        private string _text;

        public void Init(string sourceId)
        {
            _pos = 0;
            _text = sourceId;
        }
       
        public bool EndOfDataStream => string.IsNullOrEmpty(_text) || _pos >= _text.Length;

        public string GetNext()
        {
            if (EndOfDataStream) throw new IndexOutOfRangeException();
            for (; _pos < _text.Length; _pos++)
            {
                if (char.IsLetter(_text[_pos]))
                {
                    var e = _pos;
                    while (++e < _text.Length && char.IsLetter(_text[e]));
                    var str = _text.Substring(_pos, e - _pos).ToLower();
                    _pos = e;
                    return str;
                }
            }
            return null;
        }

    }
}
