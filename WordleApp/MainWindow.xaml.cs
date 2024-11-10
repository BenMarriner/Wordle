using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Authentication;
using System.Text;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WordleEngine;

namespace WordleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBlock[] guessedWordLabels;

        public MainWindow()
        {
            InitializeComponent();

            guessedWordLabels =
            [
                lbl_GuessedWordBox1,
                lbl_GuessedWordBox2,
                lbl_GuessedWordBox3,
                lbl_GuessedWordBox4,
                lbl_GuessedWordBox5,
                lbl_GuessedWordBox6
            ];

            this.Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// Submit the next guess word for validation and app update if valid
        /// </summary>
        private void SubmitGuessWord()
        {
            // Retrieve and validate input from the textboxes
            String newGuessWord = GetTextBoxInput();
            if (newGuessWord == "") return;

            // Add the latest guess word. Warn the player if it is not found in the dictionary
            try
            {
                if (App.AddGuessedWord(newGuessWord))
                {
                    App.AppUpdate();
                    txtBoxInput.Clear();
                }
                else
                {
                    throw new InvalidWordException();
                }
            }
            catch (InvalidWordException)
            {
                MessageBox.Show("This is not a valid word. Please choose a different one");
            }
            txtBoxInput.Focus();
        }

        private String GetTextBoxInput()
        {
            String textboxGuess = txtBoxInput.Text;
            try
            {
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

        /// <summary>
        /// Updates the MainWindow UI to reflect the current state of the game
        /// </summary>
        public void UIUpdate()
        {
            // Adds the current guess word to the next available guessed word text block
            // CurrGuessWord will be null at the beginning when no words yet been entered
            if (App.CurrGuessWord is not null && App.CurrGuessWord != "")
                AddToGuessedWordsList(App.CurrGuessWord);
            
            lblGuessesLeft.Content = $"Guesses Remaining: {App.NumGuessesLeft}";

            // Once the game finishes, we will disable all input from the window and display
            // a win or a loss message.
            if (App.GameFinished)
            {
                txtBoxInput.IsEnabled = false;
                btn_Submit.IsEnabled = false;

                if (App.Win)
                {
                    lbl_Outcome.Content = "Congratulations! You guessed correctly";
                }
                else
                {
                    lbl_Outcome.Content = $"Oh no! You couldn't guess the word. The correct word was {App.TargetWord}. Better luck next time!";
                }
            }
        }

        /// <summary>
        /// Adds a guess word to the next available empty textblock
        /// </summary>
        /// <param name="word"></param>
        private void AddToGuessedWordsList(Word word)
        {
            var textblock = guessedWordLabels.First(label => label.Text == "");
            if (textblock is not null)
            {
                // Write each letter in the textblock with its corresponding colour
                foreach (Letter letter in word)
                    textblock.Inlines.Add(new Run(letter.ToString()) { Foreground = ColourConverter.GetColour(letter.Colour) });
            }
        }

        /// <summary>
        /// When the submit button is clicked, the application will get the text from the textbox
        /// and pass it to the engine for validation. From here, depending on if the word comes back as validated,
        /// The WordleApp's update function is called. If the player's input is not valid, a message box will appear
        /// that will ask the user to type something else.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SubmitGuessWord();
        }

        /// <summary>
        /// Keeps all textbox input to letters only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidateTextBoxInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsLetter))
            {
                e.Handled = true;
            }
        }

        private void TextboxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitGuessWord();
            }
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Focus on the first textbox to quickly begin typing the next guess word
            txtBoxInput.Focus();
        }
    }
}