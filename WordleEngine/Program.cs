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
            SetTargetWord(inputTargetWord);
            pastGuessWords = new Word[maxGuesses];
        }

        /// <summary>
        /// Cleans and validates string input and checks if the word exists in the dictionary
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateStringInput(String input)
        {
            if (input == null) return false;
            if (input.Length != 5) return false;
            if (input.Any(c => !char.IsLetter(c))) return false;
            var validatedString = input.Trim().ToUpper();
            if (Dictionary.WordExists(validatedString)) return true;
            else return false;
        }

        public bool AddGuessedWord(String newWord)
        {
            if (!ValidateStringInput(newWord) || TargetWord is null || PastGuessWords is null) return false;

            CurrGuessWord = newWord;
            Word.SetWordLetterColours(CurrGuessWord, TargetWord);
            PastGuessWords[maxGuesses - NumGuessesLeft] = CurrGuessWord;

            return true;
        }

        public bool SetTargetWord(String newWord)
        {
            if (ValidateStringInput(newWord))
            {
                TargetWord = newWord;
                return true;
            }
            return false;
        }

        /// <summary>
        /// The engine update function checks to see if the most recent word guessed is the correct word.
        /// If it is, the engine's GameFinished and Win flags will be set so that the game can terminate.
        /// If the word is not correct and there are no more guesses left, the GameFinished flag will be
        /// set but not the Win flag.
        /// </summary>
        public void EngineUpdate()
        {
            if (targetWord is null) return;

            GameFinished = NumGuessesLeft == 0 || CurrGuessWord == TargetWord;
            Win = CurrGuessWord == TargetWord;
        }
    }
}
