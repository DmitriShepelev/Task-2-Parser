using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2.TextModel
{
    public struct Text : ITextUnit
    {
        private readonly Sentence[] textContainer;
        public Text(Sentence[] textUnits)
        {
            textContainer = textUnits;
        }

        public ITextUnit GetText { get => this; }


        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            foreach (var item in textContainer)
            {
                stringBuilder.Append(item.ToString());
            }
            return stringBuilder.ToString();
        }

        public IEnumerator<ITextUnit> GetEnumerator()
        {
            for (int i = 0; i < textContainer.Length; i++)
            {
                yield return textContainer[i];
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<ITextUnit>)GetEnumerator();
        }
    }
}
