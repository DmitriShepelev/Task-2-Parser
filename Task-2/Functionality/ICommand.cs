using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.TextModel;

namespace Task_2.Functionality
{
   public interface ICommand
    {
        public ITextUnit GetTextWithoutWorsdStartingWithConsonant(ITextUnit text, int length);
        public ITextUnit GetSentenceWhereWordsReplaceWithSubstring(ITextUnit sentence, int length, ITextUnit subString);
    }
}
