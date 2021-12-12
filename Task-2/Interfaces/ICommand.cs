namespace Task_2.Interfaces
{
   public interface ICommand
    {
        public ITextUnit GetTextWithoutWorsdStartingWithConsonant(ITextUnit text, int length);
        public ITextUnit GetSentenceWhereWordsReplaceWithSubstring(ITextUnit sentence, int length, ITextUnit subString);
    }
}
