using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task_2.Interfaces;

namespace Task_2.TextModel
{
    public struct Sentence : ITextUnit
    {
        private readonly ITextUnit[] wordsAndSpacesAndPuntuation;
        private readonly int countOfWords;
        private readonly bool isInterrogativeSentence;
        public Sentence(ITextUnit[] textUnits)
        {
            wordsAndSpacesAndPuntuation = textUnits;
            countOfWords = textUnits.OfType<Word>().Count();
            isInterrogativeSentence = textUnits.OfType<TrailingPunctuationMark>().Any(x => x.IsInterrogativePuntuationMark);
        }

        public int CountOfWords => countOfWords;
        public bool IsInterrogativeSentence => isInterrogativeSentence;

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            foreach (var item in wordsAndSpacesAndPuntuation)
            {
                stringBuilder.Append(item.ToString());
            }
            return stringBuilder.ToString();
        }

        public IEnumerator<ITextUnit> GetEnumerator()
        {
            for (int i = 0; i < wordsAndSpacesAndPuntuation.Length; i++)
            {
                yield return wordsAndSpacesAndPuntuation[i];
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator<ITextUnit>)GetEnumerator();
        }
    }
}
