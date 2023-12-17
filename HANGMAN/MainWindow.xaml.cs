using System;
using System.Windows;
using System.Windows.Controls;

namespace HANGMAN
{
    public partial class MainWindow : Window
    {
        #region Variables
        string[] hangmanArt = { "___\n", "|\n", "O\n", "/", "|", "\\\n", "/", "\\\n", "___" };
        string[] words = { "cat", "linux", "puppy", "election", "marriage", "topic", "city" };
        string selectedWord;
        char[] placeholder;
        int incorrectGuesses = 0;
        private int difficultyLevel;
        private string playerName;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            HangmanDatabase.InitializeDatabase();
            difficultyLevel = GetDifficultyLevel();


            playerName = PromptForPlayerName();

            #region Events
            checkBtn.Click += (sender, e) => CheckBtn_Click(playerName);
            word.Click += Word_Click;
            #endregion
        }

        private int GetDifficultyLevel()
        {
            if (selectedWord == null)
            {

                return 0;
            }

            if (selectedWord.Length <= 4)
            {
                return 3;
            }
            else if (selectedWord.Length <= 6)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        #region Random Word Generator
        private void Word_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            int index = random.Next(0, words.Length - 1);
            selectedWord = words[index];
            placeholder = new char[selectedWord.Length];
            guessedWord.Content = "";
            hangmanLabel.Content = "";
            incorrectGuesses = 0;

            for (int i = 0; i < placeholder.Length; i++)
            {
                placeholder[i] = '-';
                guessedWord.Content += placeholder[i].ToString();
            }
        }
        #endregion

        #region Generate
        private void CheckBtn_Click(string playerName)
        {
            if (checkbox.Text.Length == 1)
            {
                bool correctGuess = false;
                char letter = Convert.ToChar(checkbox.Text);
                for (int i = 0; i < selectedWord.Length; i++)
                {
                    if (letter == selectedWord[i])
                    {
                        correctGuess = true;
                        placeholder[i] = letter;
                    }
                }
                guessedWord.Content = new string(placeholder);
                int remainingLetters = Array.FindAll(placeholder, c => c == '-').Length;
                if (remainingLetters > 0)
                {
                    if (!correctGuess && incorrectGuesses < 9)
                    {
                        hangmanLabel.Content += hangmanArt[incorrectGuesses];
                        incorrectGuesses++;
                    }
                    if (incorrectGuesses == 9)
                    {
                        MessageBox.Show("No lives left");
                        guessedWord.Content = selectedWord;
                    }
                }
                else
                {
                    MessageBox.Show("Congratulations, you guessed the word!");

                    int score = (10 - incorrectGuesses);
                    HangmanDatabase.InsertScore(playerName, score, "difficulty");
                }
            }
            else
            {
                MessageBox.Show("Enter letters");
            }
        }

        private string GetDifficultyName(int difficulty)
        {
            switch (difficulty)
            {
                case 1:
                    return "Hard";
                case 2:
                    return "Medium";
                case 3:
                    return "Easy";
                default:
                    return "Unknown";
            }
        }
        #endregion

        private string PromptForPlayerName()
        {
            InputDialog dialog = new InputDialog("Enter Your Name", "Player Name:");
            dialog.ShowDialog();


            return dialog.Answer == "" ? "Anonymous" : dialog.Answer;
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
