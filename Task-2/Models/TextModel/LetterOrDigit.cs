using System.Collections;
using System.Collections.Generic;
using Task_2.Interfaces;

namespace Task_2.TextModel
{
    public struct LetterOrDigit : ITextUnit
    {
        private readonly char _value;

        public LetterOrDigit(char value)
        {
            _value = value;
        }
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
