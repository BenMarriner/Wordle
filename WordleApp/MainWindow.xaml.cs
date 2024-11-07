using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wordle;

namespace WordleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBox[] textBoxes;
        private TextBlock[] guessedWordLabels;
        
        public MainWindow()
        {
            InitializeComponent();
            textBoxes = 
            [
                txtbox_GuessedLetter1,
                txtbox_GuessedLetter2,
                txtbox_GuessedLetter3,
                txtbox_GuessedLetter4,
                txtbox_GuessedLetter5
            ];

            guessedWordLabels =
            [
                lbl_GuessedWordBox1,
                lbl_GuessedWordBox2,
                lbl_GuessedWordBox3,
                lbl_GuessedWordBox4,
                lbl_GuessedWordBox5,
                lbl_GuessedWordBox6
            ];
        }

        /// <summary>
        /// When the submit button is clicked, the application will assemble the word from the letters in the textboxes
        /// and pass it to the engine for validation. From here, depending on if the word comes back as validated,
        /// The UI update function will be called. If the player's input is not valid, a message box will appear
        /// where the user will be asked to type something else.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // This is here to satisfy .NET's dereferencing possible null warning
            if (App.engine is null) return;

            // Retrieve and validate input from the textboxes
            String guessInput = GetTextBoxInput();
            if (guessInput == "") return;

            // Add the latest guess word. Throw exception if not found in the dictionary
            try
            {
                if (!App.engine.AddGuessedWord(guessInput))
                    throw new InvalidWordException();
                else
                    UIUpdate();
            }
            catch (InvalidWordException)
            {
                MessageBox.Show("This is not a valid word. Please choose a different one");
            }
        }

        /// <summary>
        /// This function stops any non-letter characters from being entered into a textbox
        /// The function name is attached to the textboxes' PreviewTextInput attribute in the XAML code.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateTextBoxInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsLetter))
            {
                e.Handled = true;
            }
            else
            {
                var textbox = sender as TextBox;
                if (textbox is not null)
                {
                    textbox.Text = e.Text.ToUpper();
                    textbox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                }
                e.Handled = true;
            }
        }

        private void BackspaceTraversal(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                var textbox = sender as TextBox;
                if (textbox is not null)
                {
                    textbox.Clear();
                    textbox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Previous));
                }
            }
        }

        private String GetTextBoxInput()
        {
            String textboxGuess = "";
            try
            {
                foreach (TextBox textBox in textBoxes)
                {
                    textboxGuess += (textBox.Text);
                }

                if (textboxGuess.Length != 5) throw new ArgumentException();
                
                return textboxGuess;
            }
            catch (ArgumentException)
            {
                if (textboxGuess.Length == 0)
                    MessageBox.Show("Please enter a 5 letter word");
                else
                    MessageBox.Show(textboxGuess + " is either not a word or is not 5 letters long. Try a different one");
            }

            return "";
        }

        private void UIUpdate()
        {
            lblGuessesLeft.Content = $"Guesses Remaining: {App.numGuessesLeft}";
            foreach (TextBox textBox in textBoxes)
                textBox.Clear();

            for (int i = 0; i < App.engine.PastGuessWords.Length; i++)
            {
                guessedWordLabels[i].Text = App.engine.PastGuessWords[i];
            }
        }
    }
}