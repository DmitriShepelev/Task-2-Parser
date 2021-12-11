using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.TextModel;

namespace Task_2.Functionality
{
    class Command : ICommand
    {
        public ITextUnit GetTextWithoutWorsdStartingWithConsonant(ITextUnit text, int length)
        {
            var sentences = text.OfType<Sentence>();

            List<ITextUnit> newSentence = new();
            List<Sentence> textToReturn = new();

            foreach (var sentence in sentences)
            {
                foreach (var unit in sentence)
                {
                    if (!(unit is Word _word && _word.Length == length && _word.BeginWithConsonant))
                        newSentence.Add(unit);
                }
                textToReturn.Add(new Sentence(newSentence.ToArray()));
                newSentence.Clear();
            }

            return new Text(textToReturn.ToArray());
        }

        public ITextUnit GetSentenceWhereWordsReplaceWithSubstring(ITextUnit sentence, int length, ITextUnit subStringForTesting)
        {
            List<ITextUnit> newSentence = new();

            foreach (var unit in sentence)
            {
                if (!(unit is Word _word && _word.Length == length))
                    newSentence.Add(unit);
                else
                {
                    foreach (var item in subStringForTesting)
                    {
                        newSentence.Add(item);
                    }
                }
            }

            return new Sentence(newSentence.ToArray());
        }        
    }
}
