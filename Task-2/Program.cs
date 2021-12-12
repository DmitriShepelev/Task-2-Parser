using System;
using Task_2.TextModel;
using System.Configuration;
using System.IO;
using Task_2.Interfaces;
using System.Text.RegularExpressions;
using System.Linq;
using Task_2.Services;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string dash = new('=', 100);
            TextParser parser = new();
            ITextUnit text, subString;
            var sourceTextFilePath = ConfigurationManager.AppSettings.Get("inputFilePath");
            var correctSubString = ConfigurationManager.AppSettings.Get("correctSubStringPath");
            var outputFile = ConfigurationManager.AppSettings.Get("outputFilePath");
            //var incorrectSubString = ConfigurationManager.AppSettings.Get("incorrectSubStringPath");

            using (var reader = new StreamReader(sourceTextFilePath))
            {
                text = parser.Parse(reader);
            }

            //Displaying text read by the program on the screen
            //Console.WriteLine(text); 

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSort text by the count of words in a sentence." + "\n");
            Console.ResetColor(); Console.WriteLine(dash);

            new Query().ShowTextSortedByWordCount(text);

            Console.WriteLine(dash);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nShow words of a given length from interrogative sentences without repetitions. (length = 5)" + "\n");
            Console.ResetColor(); Console.WriteLine(dash);

            new Query().ShowWordsInInterrogativeSentenceByLength(text, 5);

            Console.WriteLine(dash);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRemove from the text all words of a given length that begin with a consonant." + "\n");
            Console.ResetColor(); Console.WriteLine(dash);

            var com = new Command().GetTextWithoutWorsdStartingWithConsonant(text, 5);
            Console.WriteLine(Regex.Replace(com.ToString(), "\\s+", " "));

            Console.WriteLine(dash);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSentence in text for insertions." + "\n");
            Console.ResetColor(); Console.WriteLine(dash);

            var sentence = text.OfType<Sentence>().Skip(6).First();
            Console.WriteLine(sentence);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nReplacing a sentence with a valid substring" + "\n");
            Console.ResetColor(); Console.WriteLine(dash);

            using var subStringReader = new StreamReader(correctSubString);
            subString = parser.Parse(subStringReader);
            var com2 = new Command().GetSentenceWhereWordsReplaceWithSubstring(sentence, 5, subString);
            Console.WriteLine(Regex.Replace(com2.ToString(), "\\s+", " ")); Console.WriteLine(dash);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + @"The full text is saved in the file ""Output.txt""." + "\n");
            Console.ResetColor(); Console.WriteLine(dash);
            new Query().SaveText(text, outputFile);
        }
    }
}
