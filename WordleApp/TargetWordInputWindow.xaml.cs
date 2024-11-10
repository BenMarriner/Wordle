using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WordleApp
{
    /// <summary>
    /// Interaction logic for TargetWordInputWindow.xaml
    /// Responsible for prompting the player to input target words
    /// </summary>
    public partial class TargetWordInputWindow : Window
    {
        public String TargetWordInput { get; private set; }

        public TargetWordInputWindow(String message = "Please choose a 5 letter target word:")
        {
            InitializeComponent();
            lblMessage.Content = message;
            TargetWordInput = "";
            this.Loaded += TargetWordInputWindow_Loaded;
        }

        private void TargetWordInputWindow_Loaded(object sender, RoutedEventArgs e)
        {
            txtboxInput.Focus();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            SubmitInput();
        }

        private void SubmitInput()
        {
            TargetWordInput = txtboxInput.Text;
            txtboxInput.Clear();
            Hide();
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

        public void SetWindowMessage(String message)
        {
            lblMessage.Content = message;
        }

        private void txtboxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SubmitInput();
            }
        }
    }
}
