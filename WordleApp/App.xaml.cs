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
        public static WordleEngine? engine;
        public static int numGuessesLeft;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            try
            {
                engine = new("NESTS");
                numGuessesLeft = engine.NumGuessesLeft;
            }
            catch (DictionaryNotFoundException ex)
            {
                MessageBox.Show("FATAL ERROR: Could not find the dictionary.txt file. Please ensure that this file exists in the root directory of the game. Wordle will now close...");
                App.Current.Shutdown(ex.HResult);
            }
        }
    }
}
