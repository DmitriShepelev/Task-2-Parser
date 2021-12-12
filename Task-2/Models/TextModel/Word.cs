using System;
using System.Collections;
using System.Collections.Generic;
using Task_2.Interfaces;

namespace Task_2.TextModel
{
    public struct Word : ITextUnit
    {
        private readonly string _value;
        private readonly int _length;
        private readonly bool beginWithConsonant;
        public Word(string value)
        {
            _value = value;
            _length = value.Length;
            string vowel = String.Intern("aeiouAEIOU");
            if (vowel.Contains(value[0]))
            {
                beginWithConsonant = false;
            }
            else
            {
                beginWithConsonant = true;
            }
        }

        public bool BeginWithConsonant { get => beginWithConsonant; }
        public int Length { get => _length; }


        public override string ToString()
        {
            return _value;
        }
        public IEnumerator<ITextUnit> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<ITextUnit>)GetEnumerator();
        }
    }
}
