using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2.TextModel
{
    public struct InternalPunctuationMark : ITextUnit
    {
        private readonly char _value;
        public InternalPunctuationMark(char value)
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
