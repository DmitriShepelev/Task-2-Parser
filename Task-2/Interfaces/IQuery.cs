namespace Task_2.Interfaces
{
    public interface IQuery
    {
        public void ShowTextSortedByWordCount(ITextUnit text);
        public void ShowWordsInInterrogativeSentenceByLength(ITextUnit text, int length);
    }
}
