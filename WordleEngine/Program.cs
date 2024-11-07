using System.Runtime.InteropServices;

namespace Wordle
{
    public class WordleEngine
    {
        private const int maxGuesses = 6;
        private Word? targetWord;
        private Word[]? pastGuessWords;
        private Word? currGuessWord;
        private bool win;

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
                return MaxGuesses - (PastGuessWords != null ? PastGuessWords.Count(word => word != null) : 0);
            }
        }
        public bool Win { get { return win; } private set { win = value; } }

        public WordleEngine(String inputTargetWord)
        {
            Dictionary.InitDictionary();
            targetWord = inputTargetWord;
            pastGuessWords = new Word[maxGuesses];
            win = false;
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
            if (!ValidateStringInput(newWord)) return false;

            pastGuessWords = pastGuessWords ?? new Word[maxGuesses];
            Word newGuessedWord = newWord;
            Word.SetWordLetterColours(newGuessedWord, targetWord ?? "");
            pastGuessWords[maxGuesses - NumGuessesLeft] = newGuessedWord;

            return true;
        }

        private void Update()
        {
            
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
