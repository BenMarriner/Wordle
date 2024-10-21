﻿namespace Wordle
{
    

    public class Wordle
    {
        static int maxGuesses;
        static int numGuessesLeft;
        static Word targetWord;
        static Word[] pastGuessWords;
        static Word currGuessWord;
        
        static void PrintGameState(Word[] wordsList)
        {
            Console.Clear();
            Console.WriteLine("=== Wordle ===");
            Console.WriteLine("Guessed words:");
            for (int i = 0; i <= maxGuesses - numGuessesLeft; i++)
            {
                Word.PrintWord(wordsList[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main()
        {
            maxGuesses = 6;
            numGuessesLeft = maxGuesses;
            targetWord = new Word("Turbo");
            pastGuessWords = new Word[maxGuesses];

            while (numGuessesLeft > 0)
            {
                // Handle user input
                String? input = "";
                do
                {
                    Console.WriteLine("Please enter a 5 letter word: ");
                    input = Console.ReadLine();
                    input = input.Trim();
                }
                while (input.Length != 5);

                currGuessWord = new Word(input);
                Word.SetWordLetterColours(currGuessWord, targetWord);
                pastGuessWords[maxGuesses - numGuessesLeft] = currGuessWord;

                PrintGameState(pastGuessWords);

                numGuessesLeft--;
            }
        }
    }
}
