using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Wordle
{
    class Word
    {
        private String _letters = "";
        private ConsoleColor[] _colours = [ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White];
        public String Letters
        { 
            get { return _letters; }
            set
            {
                // Ensures that all words entered are cleaned and validated
                if (value is String)
                {
                    String newLetters = (String)value;
                    newLetters = newLetters.ToUpper().Trim();
                    if (newLetters.Length > 5)
                        newLetters = newLetters.Substring(0, 5);
                    
                    _letters = newLetters;
                }
                else
                {
                    _letters = "";
                }
            }
        }
        public ConsoleColor[] Colours
        { 
            get 
            { 
                return _colours; 
            } 
            set 
            {
                _colours = (value != null) ? value : [ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White]; 
            } 
        }

        public Word(String inLetters, ConsoleColor[] inColours)
        {
            Letters = inLetters;
            Colours = inColours;
        }

        public Word(String inLetters)
        {
            Letters = inLetters;
        }

        // Evaluates the guess word against the target word and sets the colours of the letters
        // Green = Correct letter in correct position
        // Yellow = Letter is found in the word but not in the correct location
        // Red = Letter is not found in the word
        public static void SetWordLetterColours(Word guessWord, Word targetWord)
        {
            if (guessWord._letters.Length != targetWord._letters.Length)
                throw new Exception("EvaluateWord: Guess word and target word do not have the same length");

            ConsoleColor[] evaluatedColours = new ConsoleColor[5];

            for (int i = 0; i < targetWord._letters.Length; i++)
            {
                if (Char.Equals(guessWord._letters[i], targetWord._letters[i]))
                    evaluatedColours[i] = ConsoleColor.Green;
                else if (targetWord._letters.Contains(guessWord._letters[i]))
                    evaluatedColours[i] = ConsoleColor.Yellow;
                else
                    evaluatedColours[i] = ConsoleColor.Red;
            }

            guessWord._colours = evaluatedColours;
        }

        public static void PrintWord(Word word)
        {
            for (int i = 0; i < word.Letters.Length; i++)
            {
                Console.ForegroundColor = word._colours[i];
                Console.Write(word._letters[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        public override string ToString()
        {
            return Letters;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Word word)
            {
                return this.Letters == word.Letters;
            }
            else
            {
                return false;
            }
        }
    }
}
