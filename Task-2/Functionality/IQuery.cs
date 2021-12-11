using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_2.TextModel;

namespace Task_2.Functionality
{
    public interface IQuery
    {
        public void ShowTextSortedByWordCount(ITextUnit text);
        public void ShowWordsInInterrogativeSentenceByLength(ITextUnit text, int length);
    }
}
