using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Task_2.TextModel;
using Task_2.Interfaces;

namespace Task_2.Services
{
    class Query : IQuery
    {
        public void ShowTextSortedByWordCount(ITextUnit text)
        {
            var result = text.OfType<Sentence>().OrderBy(x => x.CountOfWords);
            foreach (var item in result)
            {
                Console.WriteLine(Regex.Replace(item.ToString().Trim(), "\\s+", " "));
            }
        }

        public void ShowWordsInInterrogativeSentenceByLength(ITextUnit text, int length)
        {
            var sentences = text.OfType<Sentence>().Where(s => s.IsInterrogativeSentence);
            List<Word> words = new();
            foreach (var sentence in sentences)
            {
                foreach (var word in sentence)
                {
                   if(word is Word _word && _word.Length == length) words.Add(_word);
                }
            }
            var wordsResult = words.Distinct();
            foreach (var word in wordsResult)
            {
                Console.WriteLine(word);
            }
        }
        public void SaveText(ITextUnit text, string path)
        {
            using StreamWriter writer = new(path);
            foreach (var sentence in text)
            {
                foreach (var unit in sentence)
                {
                    writer.Write(unit);
                }
            }
        }
    }
}
