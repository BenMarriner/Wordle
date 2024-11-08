using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Wordle
{
    public class WordleEngine
    {
        private const int maxGuesses = 6;
        private Word targetWord = "";
        private Word currGuessWord = "";
        private Word[]? pastGuessWords;
        private bool gameFinished = false;
        private bool win = false;

        public int MaxGuesses { get { return maxGuesses; } }
        public Word[]? PastGuessWords
        { 
            get 
            { 
                pastGuessWords = pastGuessWords ?? new Word[MaxGuesses];
                return pastGuessWords;
            }
        }
        public int NumGuessesLeft
        {
            get
            {
                if (PastGuessWords is null) return MaxGuesses;
                return PastGuessWords.Count(word => word is null);
            }
        }
        public Word TargetWord
        {
            get
            {
                return targetWord;
            }
            private set
            {
                targetWord = value;
            }
        }
        public Word CurrGuessWord { get { return currGuessWord; } private set { currGuessWord = value; } }
        
        public bool GameFinished { get { return gameFinished; } private set { gameFinished = value; } }
        public bool Win { get { return win; } private set { win = value; } }


        public WordleEngine(String inputTargetWord)
        {
            Dictionary.InitDictionary();
            targetWord = inputTargetWord;
            pastGuessWords = new Word[maxGuesses];
        }

        /// <summary>
        /// Prints the list of words the player has guessed so far
        /// </summary>
        //private static void PrintGameState()
        //{
        //    pastGuessWords = pastGuessWords ?? new Word[maxGuesses];

        //    Console.Clear();
        //    Console.WriteLine("=== Wordle ===");
        //    Console.WriteLine("Guessed words:");
        //    for (int i = 0; i < pastGuessWords.Length; i++)
        //    {
        //        Word? word = pastGuessWords[i];
        //        if (word != null)
        //            Word.PrintWord(pastGuessWords[i]);
        //    }
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine();
        //}

        /// <summary>
        /// The screen for if the player wins
        /// </summary>
        //private static void PrintWinScreen()
        //{
        //    PrintGameState();
        //    Console.WriteLine("Congratulations! You guessed correctly");
        //}

        /// <summary>
        /// The screen for if the player loses
        /// </summary>
        //private static void PrintLossScreen()
        //{
        //    PrintGameState();
        //    Console.WriteLine("Oh no! You couldn't guess the word. Better luck next time!");
        //    Console.WriteLine("The correct word was " + targetWord);
        //}

        /// <summary>
        /// This function handles and validates input from the player
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        //private static String ReceiveWordInput(String message = "Please enter a 5 letter word: ") // If no custom message is provided, display the default one
        //{
        //    String input;
        //    bool wordExists = false;
        //    bool correctLength = false;

        //    do
        //    {
        //        Console.WriteLine(message);
        //        input = Console.ReadLine() ?? String.Empty;
        //        input = input.Trim();

        //        // Validation checks
        //        wordExists = Dictionary.WordExists(input);
        //        correctLength = input.Length == 5;

        //        if (!wordExists || !correctLength)
        //            Console.WriteLine(input + " is either not a word or is not 5 letters long. Try a different one");
        //    }
        //    while (!(wordExists && correctLength));

        //    return input;
        //}

        /// <summary>
        /// Cleans and validates string input and checks if the word exists in the dictionary
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private bool ValidateStringInput(String input)
        {
            //if (input == null) return false;
            //if (input.Length != 5) return false;
            //if (input.Any(c => !char.IsLetter(c))) return false;
            //var validatedString = input.Trim().ToUpper();
            //if (Dictionary.WordExists(input)) return true;
            //else return false;
            return Dictionary.WordExists(input);
        }

        public bool AddGuessedWord(String newWord)
        {
            if (!ValidateStringInput(newWord) || TargetWord is null || PastGuessWords is null) return false;

            CurrGuessWord = newWord;
            Word.SetWordLetterColours(CurrGuessWord, TargetWord);
            PastGuessWords[maxGuesses - NumGuessesLeft] = CurrGuessWord;

            Update();

            return true;
        }

        /// <summary>
        /// The engine update function checks to see if the most recent word guessed is the correct word.
        /// If it is, the engine's GameFinished and Win flags will be set so that the game can terminate.
        /// If the word is not correct and there are no more guesses left, the GameFinished flag will be
        /// set but not the Win flag.
        /// </summary>
        private void Update()
        {
            if (targetWord is null) return;

            GameFinished = NumGuessesLeft == 0 || CurrGuessWord == TargetWord;
            Win = CurrGuessWord == TargetWord;
        }

        //public static void Main()
        //{
        //    // Initialise word dictionary
        //    Dictionary.InitDictionary();

        //    numGuessesLeft = maxGuesses;
        //    pastGuessWords = new Word[maxGuesses];
        //    targetWord = new Word(ReceiveWordInput("Please enter the target word for the player guess: "));

        //    while (numGuessesLeft > 0)
        //    {
        //        PrintGameState();

        //        // Handle user input
        //        currGuessWord = new Word(ReceiveWordInput());
        //        Word.SetWordLetterColours(currGuessWord, targetWord);

        //        pastGuessWords[maxGuesses - numGuessesLeft] = currGuessWord;


        //        if (currGuessWord == targetWord)
        //        {
        //            win = true;
        //            break;
        //        }
        //        else
        //        {
        //            numGuessesLeft--;
        //        }
        //    }

        //    if (win)    PrintWinScreen();
        //    else        PrintLossScreen();
        //}
    }
}
