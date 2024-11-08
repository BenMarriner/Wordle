using System.Configuration;
using System.Data;
using System.Windows;
using Wordle;

namespace WordleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static WordleEngine? engine;
        public static Word currGuessWord = "";
        public static Word targetWord = "";
        public static int numGuessesLeft;
        public static bool gameFinished;
        public static bool win;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            try
            {
                engine = new("NESTS");
                AppUpdate();
            }
            catch (DictionaryNotFoundException ex)
            {
                MessageBox.Show("FATAL ERROR: Could not find the dictionary.txt file. Please ensure that this file exists in the root directory of the game. Wordle will now close...");
                App.Current.Shutdown(ex.HResult);
            }
        }

        public static void AppUpdate()
        {
            if (engine is null) return;

            currGuessWord = engine.CurrGuessWord;
            targetWord = engine.TargetWord;
            numGuessesLeft = engine.NumGuessesLeft;
            gameFinished = engine.GameFinished;
            win = engine.Win;
        }

        public static bool AddGuessedWord(String word)
        {
            if (engine is null) return false;
            return engine.AddGuessedWord(word);
        }
    }
}
