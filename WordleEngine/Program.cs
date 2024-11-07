namespace Wordle
{
    public class WordleEngine
    {
        private static int maxGuesses = 6;
        private static int numGuessesLeft;
        private static Word? targetWord;
        private static Word[]? pastGuessWords;
        private static Word? currGuessWord;
        private static bool win;
        
        /// <summary>
        /// Prints the list of words the player has guessed so far
        /// </summary>
        private static void PrintGameState()
        {
            pastGuessWords = pastGuessWords ?? new Word[maxGuesses];
            
            Console.Clear();
            Console.WriteLine("=== Wordle ===");
            Console.WriteLine("Guessed words:");
            for (int i = 0; i < pastGuessWords.Length; i++)
            {
                Word? word = pastGuessWords[i];
                if (word != null)
                    Word.PrintWord(pastGuessWords[i]);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// The screen for if the player wins
        /// </summary>
        private static void PrintWinScreen()
        {
            PrintGameState();
            Console.WriteLine("Congratulations! You guessed correctly");
        }

        /// <summary>
        /// The screen for if the player loses
        /// </summary>
        private static void PrintLossScreen()
        {
            PrintGameState();
            Console.WriteLine("Oh no! You couldn't guess the word. Better luck next time!");
            Console.WriteLine("The correct word was " + targetWord);
        }
        
        /// <summary>
        /// This function handles and validates input from the player
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private static String ReceiveWordInput(String message = "Please enter a 5 letter word: ") // If no custom message is provided, display the default one
        {
            String input;
            bool wordExists = false;
            bool correctLength = false;

            do
            {
                Console.WriteLine(message);
                input = Console.ReadLine() ?? String.Empty;
                input = input.Trim();

                // Validation checks
                wordExists = Dictionary.WordExists(input);
                correctLength = input.Length == 5;

                if (!wordExists || !correctLength)
                    Console.WriteLine(input + " is either not a word or is not 5 letters long. Try a different one");
            }
            while (!(wordExists && correctLength));

            return input;
        }

        public static void Main()
        {
            // Initialise word dictionary
            Dictionary.InitDictionary();

            numGuessesLeft = maxGuesses;
            pastGuessWords = new Word[maxGuesses];
            targetWord = new Word(ReceiveWordInput("Please enter the target word for the player guess: "));

            while (numGuessesLeft > 0)
            {
                PrintGameState();

                // Handle user input
                currGuessWord = new Word(ReceiveWordInput());
                Word.SetWordLetterColours(currGuessWord, targetWord);

                pastGuessWords[maxGuesses - numGuessesLeft] = currGuessWord;


                if (currGuessWord == targetWord)
                {
                    win = true;
                    break;
                }
                else
                {
                    numGuessesLeft--;
                }
            }

            if (win)    PrintWinScreen();
            else        PrintLossScreen();
        }

    }
}
