using System;
using System.Collections.Generic;
using Task_2.TextModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Threading;

namespace Task_2.Parser
{
    public class TextParser : IParser
    {
        private ITextUnit text;
        private readonly StringBuilder wordBuilder;
        private readonly List<ITextUnit> sentenceBuilder;
        private readonly List<Sentence> textBuilder;
        private readonly Dictionary<Tuple<State, CharType>, Func<char, State>> dictionaryActions;
        private readonly string trailingPunctuation = String.Intern(".!?");
        private readonly string logPath = ConfigurationManager.AppSettings.Get("logerFilePath");
        private State State { get; set; }

        public TextParser()
        {
            wordBuilder = new();
            sentenceBuilder = new();
            textBuilder = new();
            dictionaryActions = new Dictionary<Tuple<State, CharType>, Func<char, State>>();
            SetDictionary();
        }

        public bool TryParse(StreamReader streamReader)
        {
            try
            {
                Parse(streamReader);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }

            return true;
        }

        public ITextUnit Parse(StreamReader streamReader)
        {
            State = State.Neutral;
            CharType charType;
            char readableChar;
            while (streamReader.Peek() >= 0)
            {
                readableChar = (char)streamReader.Read();
                charType = CheckChar(readableChar);
                try
                {
                    State = dictionaryActions[new Tuple<State, CharType>(State, charType)](readableChar);
                }
                catch (KeyNotFoundException e)
                {
                    using var sw = new StreamWriter(logPath);
                    var log = new Word("I cannot process this text. There should not be any punctuation marks after the space in the text. " + e.Message);
                    sw.WriteLine(log.ToString());
                    Console.WriteLine(log);
                    return log;
                }
            }
            if (wordBuilder.Length != 0)
            {
                sentenceBuilder.Add(new Word(wordBuilder.ToString()));
                wordBuilder.Clear();
            }
            if (sentenceBuilder.Count != 0)
            {
                textBuilder.Add(new Sentence(sentenceBuilder.ToArray()));
                sentenceBuilder.Clear();
            }

            text = new Text(textBuilder.ToArray());
            textBuilder.Clear();
            State = State.Neutral;
            return text;
        }


        private CharType CheckChar(char ch)
        {
            if (State == State.Word)
            {
                if (char.IsLetterOrDigit(ch) || ch == '\'' || ch == '-') return CharType.LetterOrDigit;
            }
            if (char.IsLetterOrDigit(ch)) return CharType.LetterOrDigit;
            if (char.IsWhiteSpace(ch)) return CharType.WhiteSpace;
            if (char.IsControl(ch)) return CharType.ControlSymbol;

            if (char.IsPunctuation(ch))
            {
                if (trailingPunctuation.Contains(ch)) return CharType.TrailingPunctuation;
                else return CharType.InternalPunctuation;
            }

            return CharType.Unknown;
        }

        private void SetDictionary()
        {
            dictionaryActions.Add(new Tuple<State, CharType>(State.Neutral, CharType.LetterOrDigit), AddLetterOrDigitToWord);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Neutral, CharType.WhiteSpace), AddWhiteSpaceToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Neutral, CharType.ControlSymbol), AddControlSymbolToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Word, CharType.LetterOrDigit), AddLetterOrDigitToWord);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Word, CharType.WhiteSpace), AddWordAndWhiteSpace);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Word, CharType.InternalPunctuation), AddWordAndInternalPuntuation);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Word, CharType.ControlSymbol), AddWordAndControlSymbol);
            dictionaryActions.Add(new Tuple<State, CharType>(State.Word, CharType.TrailingPunctuation), AddWordAndTrailingPunctuation);
            dictionaryActions.Add(new Tuple<State, CharType>(State.WhiteSpace, CharType.WhiteSpace), AddWhiteSpaceToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.WhiteSpace, CharType.LetterOrDigit), AddLetterOrDigitToWord);
            dictionaryActions.Add(new Tuple<State, CharType>(State.WhiteSpace, CharType.ControlSymbol), AddControlSymbolToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.InternalPunctuation, CharType.WhiteSpace), AddWhiteSpaceToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.InternalPunctuation, CharType.ControlSymbol), AddControlSymbolToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.ControlSymbol, CharType.LetterOrDigit), AddLetterOrDigitToWord);
            dictionaryActions.Add(new Tuple<State, CharType>(State.ControlSymbol, CharType.WhiteSpace), AddWhiteSpaceToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.ControlSymbol, CharType.ControlSymbol), AddControlSymbolToSentence);
            dictionaryActions.Add(new Tuple<State, CharType>(State.TrailingPunctuation, CharType.TrailingPunctuation), AddTrailingPunctuation);
            dictionaryActions.Add(new Tuple<State, CharType>(State.TrailingPunctuation, CharType.WhiteSpace), AddSentenceAndSpace);
            dictionaryActions.Add(new Tuple<State, CharType>(State.TrailingPunctuation, CharType.ControlSymbol), AddSentenceAndControlSymbol);

        }


        private State AddLetterOrDigitToWord(char ch)
        {
            wordBuilder.Append(ch);
            return State.Word;
        }

        private State AddWordAndWhiteSpace(char ch)
        {
            sentenceBuilder.Add(new Word(wordBuilder.ToString()));
            sentenceBuilder.Add(new WhiteSpace(ch));
            wordBuilder.Clear();
            return State.WhiteSpace;
        }

        private State AddWordAndInternalPuntuation(char ch)
        {
            sentenceBuilder.Add(new Word(wordBuilder.ToString()));
            sentenceBuilder.Add(new InternalPunctuationMark(ch));
            wordBuilder.Clear();
            return State.InternalPunctuation;
        }

        private State AddWordAndControlSymbol(char ch)
        {
            sentenceBuilder.Add(new Word(wordBuilder.ToString()));
            sentenceBuilder.Add(new ControlSymbol(ch));
            wordBuilder.Clear();
            return State.ControlSymbol;
        }

        private State AddControlSymbolToSentence(char ch)
        {
            sentenceBuilder.Add(new ControlSymbol(ch));
            return State.ControlSymbol;
        }

        private State AddWordAndTrailingPunctuation(char ch)
        {
            sentenceBuilder.Add(new Word(wordBuilder.ToString()));
            sentenceBuilder.Add(new TrailingPunctuationMark(ch));
            wordBuilder.Clear();
            return State.TrailingPunctuation;
        }

        private State AddWhiteSpaceToSentence(char ch)
        {
            sentenceBuilder.Add(new WhiteSpace(ch));
            return State.WhiteSpace;
        }

        private State AddTrailingPunctuation(char ch)
        {
            sentenceBuilder.Add(new TrailingPunctuationMark(ch));
            return State.TrailingPunctuation;
        }

        private State AddSentenceAndSpace(char ch)
        {
            sentenceBuilder.Add(new WhiteSpace(ch));
            textBuilder.Add(new Sentence(sentenceBuilder.ToArray()));
            sentenceBuilder.Clear();
            return State.WhiteSpace;
        }

        private State AddSentenceAndControlSymbol(char ch)
        {
            sentenceBuilder.Add(new ControlSymbol(ch));
            textBuilder.Add(new Sentence(sentenceBuilder.ToArray()));
            sentenceBuilder.Clear();
            return State.ControlSymbol;
        }
    }
}
