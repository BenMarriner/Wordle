using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Wordle
{
    /// <summary>
    /// The Word class stores a single word as well as the colours of the indivual letters.
    /// It also contains helper functions for printing the words to the console as well as
    /// checking a guess word against a target word to determine the guess word's colours.
    /// </summary>
    public class Word
    {
        private String _letters = "";
        private ConsoleColor[] _colours = [ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White];
        
        // Stores the letters
        public String Letters
        { 
            get { return _letters; }
            set
            {
                // Ensures that all words entered are cleaned and validated
                if (value is String newLetters)
                {
                    newLetters = newLetters.ToUpper().Trim();
                    if (newLetters.Length > 5)
                        newLetters = newLetters.Substring(0, 5);

                    _letters = newLetters;
                }
                else
                {
                    _letters = String.Empty;
                }
            }
        }

        // Stores the colours for each letter
        public ConsoleColor[] Colours
        { 
            get 
            { 
                return _colours; 
            } 
            set 
            {
                _colours = value ?? [ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White];
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
            ConsoleColor[] evaluatedColours = new ConsoleColor[5];

            for (int i = 0; i < targetWord.Letters.Length; i++)
            {
                if (Char.Equals(guessWord.Letters[i], targetWord.Letters[i]))
                    evaluatedColours[i] = ConsoleColor.Green;
                else if (targetWord.Letters.Contains(guessWord.Letters[i]))
                    evaluatedColours[i] = ConsoleColor.Yellow;
                else
                    evaluatedColours[i] = ConsoleColor.Red;
            }

            guessWord.Colours = evaluatedColours;
        }

        public static void PrintWord(Word word)
        {
            for (int i = 0; i < word.Letters.Length; i++)
            {
                Console.ForegroundColor = word.Colours[i];
                Console.Write(word.Letters[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// Gets a random word from the dictionary and creates a word out of it
        /// </summary>
        /// <returns></returns>
        //public static Word GenerateRandomWord()
        //{
        //    Random random = new Random();
        //    int randIdx = random.Next(0, Dictionary.WordDictionary.Count - 1);
        //    return new Word(Dictionary.WordDictionary[randIdx]);
        //}

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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
