using System.Collections;
using System.Collections.Generic;
using Task_2.Interfaces;

namespace Task_2.TextModel
{
    public struct TrailingPunctuationMark : ITextUnit
    {
        private readonly char _value;
        private readonly bool isInterrogativePuntuationMark;
        public TrailingPunctuationMark(char value)
        {
            _value = value;
            isInterrogativePuntuationMark = value == '?';
        }

        public bool IsInterrogativePuntuationMark { get => isInterrogativePuntuationMark; }

        public override string ToString()
        {
            return _value.ToString();
        }
        public IEnumerator<ITextUnit> GetEnumerator()
        {
                yield return this;
        }

        private IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }
    }
}
