using System.Configuration;
using System.Data;
using System.Windows;
using WordleEngine;

namespace WordleApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static WordleEngine.Engine? engine;
        private static MainWindow? mainWindow;
        public static Word CurrGuessWord = "";
        public static Word TargetWord = "";
        public static int NumGuessesLeft;
        public static bool GameFinished;
        public static bool Win;
        private bool quitRequestedDuringTargetWordInput = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (mainWindow is null)
                RegisterMainWindow(new MainWindow());

            try
            {
                // Initialise the dictionary early so we can validate the target word
                Dictionary.InitDictionary();

                String newTargetWord = "";
                bool isValidWord = false;
                var inputWindow = new TargetWordInputWindow();
                // Bind a function to this delegate in case the player closes the input window via the X button.
                inputWindow.Closed += InputWindow_Closed;

                // If we have a commandline argument, set newTargetWord to that
                // and validate to see if we can skip the validation loop that follows
                if (Environment.GetCommandLineArgs().Length > 1)
                {
                    newTargetWord = Environment.GetCommandLineArgs()[1];
                    isValidWord = WordleEngine.Engine.ValidateStringInput(newTargetWord);
                }

                // Continue prompting the player until a valid word is typed
                while (!isValidWord && !quitRequestedDuringTargetWordInput)
                {
                    if (String.IsNullOrEmpty(newTargetWord))
                        inputWindow.SetWindowMessage("Please enter a 5 letter target word:");
                    else
                        inputWindow.SetWindowMessage("The target word you entered is not a valid word. Please choose another:");

                    inputWindow.ShowDialog();

                    newTargetWord = inputWindow.TargetWordInput;
                    isValidWord = WordleEngine.Engine.ValidateStringInput(newTargetWord);
                }

                // If the player has clicked the input window's X button, close Wordle
                if (quitRequestedDuringTargetWordInput)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    // Unbind the function from the delegate so that closing the input window doesn't terminate the program
                    inputWindow.Closed -= InputWindow_Closed;
                    inputWindow.Close();

                    if (mainWindow is not null) mainWindow.Show();
                    engine = new(newTargetWord);
                    AppUpdate();
                }
            }
            catch (DictionaryNotFoundException ex)
            {
                MessageBox.Show("FATAL ERROR: Could not find the dictionary.txt file. Please ensure that this file exists in the root directory of the game. Wordle will now close...");
                App.Current.Shutdown(ex.HResult);
            }
        }

        /// <summary>
        /// If the player closes the input window via the X button, the whole program will terminate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputWindow_Closed(object? sender, EventArgs e)
        {
            quitRequestedDuringTargetWordInput = true;
        }

        /// <summary>
        /// The global Update function for Wordle. It updates the engine and then the frontend UI.
        /// </summary>
        public static void AppUpdate()
        {
            if (engine is null) return;

            // Update the engine to process the latest guess word
            engine.EngineUpdate();

            // Update frontend fields before we update the UI
            CurrGuessWord = engine.CurrGuessWord;
            TargetWord = engine.TargetWord;
            NumGuessesLeft = engine.NumGuessesLeft;
            GameFinished = engine.GameFinished;
            Win = engine.Win;

            // Update the main window UI
            if (mainWindow is not null)
                mainWindow.UIUpdate();
        }

        /// <summary>
        /// Registers new guess words with the engine. Must be called before AppUpdate()
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool AddGuessedWord(String word)
        {
            if (engine is null) return false;
            return engine.AddGuessedWord(word);
        }

        public static void RegisterMainWindow(MainWindow window)
        {
            mainWindow = window;
            Current.MainWindow = mainWindow;
        }
    }
}
