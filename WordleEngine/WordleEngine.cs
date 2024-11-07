using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Wordle
{
    /// <summary>
    /// This class stores information regarding a single letter.
    /// Holds the symbol and the colour, which defaults to white
    /// </summary>
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

        /// <summary>
        /// Evaluates the guess word against the target word and sets the colours of the letters
        /// Green = Correct letter in correct position
        /// Yellow = Letter is found in the word but not in the correct location
        /// Red = Letter is not found in the word
        /// </summary>
        /// <param name="guessWord"></param>
        /// <param name="targetWord"></param>
        public static void SetWordLetterColours(Word guessWord, Word targetWord)
        {
            if (guessWord == null || targetWord == null) return;

            // Here, we are constructing a table made of tuples.
            // Each tuple contains the following:
            // (Item1: Target Letters, Item2: Guess Letters, Item 3: Whether or not the letter has been discovered (true or false), Item 4: Letter colour)
            // Each tuple starts off with the letters of the guess and target words, a false value (the letter starts as undiscovered) and the colour red
            // (so that later on, if the letter is not found, it stays red).
            var targetLetters = targetWord.Letters;
            var guessLetters = guessWord.Letters;
            var discoveredLetters = Enumerable.Repeat(false, targetWord.Letters.Length).ToArray();
            var letterColours = Enumerable.Repeat(ConsoleColor.Red, targetWord.Letters.Length).ToArray();
            var evalTable = ZipExtensions.ZipFour(targetLetters, guessLetters, discoveredLetters, letterColours).ToArray();

            // Find all letters that match, mark them as discovered and set the guessed letter's colour to green
            evalTable = evalTable.Select(tuple => tuple.Target == tuple.Guess ? (tuple.Target, tuple.Guess, true, ConsoleColor.Green) : tuple).ToArray();
            
            // Find all letters from the guess word that exist in the target word but are not in the correct position
            for (int i = 0; i < evalTable.Length; i++)
            {
                // Ensure that the letter we are about to check hasn't already been discovered (and therefore marked green)
                // We are ensuring we only check undiscovered letters so that green letters don't get marked yellow.
                // Also, we only want to mark as many of a particular letter in a guessed word as there are occurrences of that
                // same letter in the target word. For instance, if the target word is NESTS and the player enters EAGLE,
                // we only want the program to highlight the first E from EAGLE in yellow since NESTS only contains one E.
                if (evalTable[i].Discovered) continue;

                var guessLetter = evalTable[i].Guess;

                for (int j = 0; j < evalTable.Length; j++)
                {
                    if (guessLetter == evalTable[j].Target && !evalTable[j].Discovered)
                    {
                        // Change the guessed letter to yellow
                        evalTable[i] = (evalTable[i].Target, evalTable[i].Guess, evalTable[i].Discovered, ConsoleColor.Yellow);
                        // Mark the target letter as discovered
                        evalTable[j] = (evalTable[j].Target, evalTable[j].Guess, true, evalTable[j].Colour);
                    }
                }
            }

            // Set the actual colour of each letter to its respective colour from the evaluation tuple
            foreach (var (Target, Guess, Discovered, Colour) in evalTable)
                Guess.Colour = Colour;
        }

        /// <summary>
        /// Prints the word with each letter in its respective colour
        /// </summary>
        /// <param name="word"></param>
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
