using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Wordle
{
    public class Letter
    {
        private char _character;
        private ConsoleColor _colour = ConsoleColor.White;

        // Letter symbol
        public char Character { get { return _character; } set { _character = char.ToUpper(value); } }
        // Letter colour when printed in the terminal
        public ConsoleColor Colour { get { return _colour; } set { _colour = value; } }

        public Letter(char inLetter) : this(inLetter, ConsoleColor.White)
        {
        }

        public Letter(char inLetter, ConsoleColor inColour)
        {
            Character = inLetter;
            Colour = inColour;
        }

        public override string ToString()
        {
            return Character.ToString();
        }

        public static implicit operator Letter(char inLetter) { return new Letter(inLetter); }
        public static bool operator ==(Letter? left, Letter? right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Character == right.Character;
        }
        public static bool operator !=(Letter left, Letter right)
        {
            return !(left == right);
        }
        
        public override bool Equals(object? obj)
        {
            if (!(obj is Letter letter)) return false;
            else return this == letter;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Character);
        }
    }
    
    /// <summary>
    /// The Word class stores a single word as well as the colours of the indivual letters.
    /// It also contains helper functions for printing the words to the console as well as
    /// checking a guess word against a target word to determine the guess word's colours.
    /// </summary>
    public class Word
    {
        private Letter[]? _letters;

        // Stores the letters
        public Letter[] Letters
        { 
            get
            {
                if (_letters == null)
                    _letters = new Letter[5];
                return _letters;
            }
        }

        public Word(String inLetters, ConsoleColor[] inColours)
        {
            int i = 0;
            foreach (var letter in inLetters)
            {
                Letters[i] = letter;
                Letters[i].Colour = inColours[i];
                i++;
            }
        }

        public Word(String inLetters) : this(inLetters, [ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White, ConsoleColor.White])
        {
        }

        // Evaluates the guess word against the target word and sets the colours of the letters
        // Green = Correct letter in correct position
        // Yellow = Letter is found in the word but not in the correct location
        // Red = Letter is not found in the word
        public static void SetWordLetterColours(Word guessWord, Word targetWord)
        {
            //ConsoleColor[] evaluatedColours = new ConsoleColor[5];

            //for (int i = 0; i < targetWord.Letters.Length; i++)
            //{
            //    if (Char.Equals(guessWord.Letters[i], targetWord.Letters[i]))
            //        evaluatedColours[i] = ConsoleColor.Green;
            //    else if (targetWord.Letters.Contains(guessWord.Letters[i]))
            //        evaluatedColours[i] = ConsoleColor.Yellow;
            //    else
            //        evaluatedColours[i] = ConsoleColor.Red;
            //}

            //guessWord.Colours = evaluatedColours;

            bool[] discoveredTargetLetters = [false, false, false, false, false];
            //var zip = targetWord.Letters.Zip(guessWord.Letters, new bool[] { false, false, false, false, false });

            //foreach (var letterTuple in zip)
            //{

            //}



            // Discover correct letters first
            for (int i = 0; i < guessWord.Letters.Length; i++)
            {
                var guessLetter = guessWord.Letters[i];
                var targetLetter = targetWord.Letters[i];
                if (guessLetter == targetLetter)
                {
                    discoveredTargetLetters[i] = true;
                    guessWord.Letters[i].Colour = ConsoleColor.Green;
                }
            }

            for (int i = 0; i < guessWord.Letters.Length; i++)
            {
                var guessLetter = guessWord.Letters[i];
                var targetLetter = targetWord.Letters[i];

                if (guessLetter != targetLetter)
                {
                    // Set colour to red in case the letter is nowhere to be found
                    guessWord.Letters[i].Colour = ConsoleColor.Red;

                    for (int j = 0; j < targetWord.Letters.Length; j++)
                    {
                        if (guessLetter == targetWord.Letters[j] && !discoveredTargetLetters[j])
                        {
                            discoveredTargetLetters[j] = true;
                            guessWord.Letters[i].Colour = ConsoleColor.Yellow;
                            break;
                        }
                    }
                }
            }
        }
            

        public static void PrintWord(Word word)
        {
            foreach (var letter in word.Letters)
            {
                Console.ForegroundColor = letter.Colour;
                Console.Write(letter);
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
            String newString = String.Empty;
            foreach (var letter in Letters) newString += letter;
            return newString;
        }

        public static implicit operator String(Word? word) { return (word is not null) ? word.ToString() : String.Empty; }
        public static implicit operator Word(String? inString) { return (inString is not null) ? inString : String.Empty; }

        public static bool operator ==(Word? word1, Word? word2)
        {
            if (ReferenceEquals(word1, word2)) return true;
            if (word1 is null || word2 is null) return false;
            return Enumerable.SequenceEqual(word1.ToString(), word2.ToString());
        }

        public static bool operator !=(Word? word1, Word? word2)
        {
            return !(word1 == word2);
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Word word)) return false;
            else return this == word;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Letters);
        }

    }
}
