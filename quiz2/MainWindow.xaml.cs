using quiz2.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Animation;

namespace quiz2
{
    public partial class MainWindow : Window
    {
        private List<Question> questions = new List<Question>();
        private int currentQuestionIndex = 0;
        private int score = 0;

        public MainWindow()
        {
            InitializeComponent();
            LoadQuestions();
            ShowQuestion();
        }

        public static ImageSource ConvertBitmapToImageSource(System.Drawing.Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void LoadQuestions()
        {

            var apple = quiz2.Properties.Resources.apple;
            var _7 = quiz2.Properties.Resources._7;
            var toyota = quiz2.Properties.Resources.toyota;
            var bk = quiz2.Properties.Resources.bk;
            var bose = quiz2.Properties.Resources.bose;
            var xiaomi = quiz2.Properties.Resources.xiaomi;
            var samsung = quiz2.Properties.Resources.samsung;
            var linux = quiz2.Properties.Resources.linux;
            var x = quiz2.Properties.Resources.X;
            var action = quiz2.Properties.Resources.action;

            // Exemple d'ajout de questions
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "eleven7", "7eleven", "el7even", "7eleven7" },
                CorrectAnswer = 1,
                QuestionImage = ConvertBitmapToImageSource(_7)
            });
            // Ajoutez ici les autres questions
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "pomme", "the POMME", "poire", "apple" },
                CorrectAnswer = 3,
                QuestionImage = ConvertBitmapToImageSource(apple)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "toiyoto", "toutita", "toyota", "tayota" },
                CorrectAnswer = 2,
                QuestionImage = ConvertBitmapToImageSource(toyota)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "Kurber Bing", "KB", "tacos land", "burger king" },
                CorrectAnswer = 3,
                QuestionImage = ConvertBitmapToImageSource(bk)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "bose", "boise", "buse", "beso" },
                CorrectAnswer = 0,
                QuestionImage = ConvertBitmapToImageSource(bose)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "xiaoiami", "mixioa", "xioa xioa", "xiaomi" },
                CorrectAnswer = 3,
                QuestionImage = ConvertBitmapToImageSource(xiaomi)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "samsung", "sungsamg", "apple", "michelin" },
                CorrectAnswer = 0,
                QuestionImage = ConvertBitmapToImageSource(samsung)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "twitter", "X", "l'oiseau", "tik tok" },
                CorrectAnswer = 1,
                QuestionImage = ConvertBitmapToImageSource(x)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "acton", "piston", "action", "tionac" },
                CorrectAnswer = 2,
                QuestionImage = ConvertBitmapToImageSource(action)
            });
            questions.Add(new Question
            {
                Text = "Quelle est le nom de cette marque ?",
                Answers = new List<string> { "linoux", "pubuntu", "inul", "linux" },
                CorrectAnswer = 3,
                QuestionImage = ConvertBitmapToImageSource(linux)
            });
            // Randomiser les questions
            questions = questions.OrderBy(q => Guid.NewGuid()).ToList();
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex  < questions.Count)
            {
                var question = questions[currentQuestionIndex];
                txtQuestion.Text = question.Text;
                ans1.Content = question.Answers[0];
                ans2.Content = question.Answers[1];
                ans3.Content = question.Answers[2];
                ans4.Content = question.Answers[3]; // Assurez-vous que vous avez ans4 dans votre XAML
                qImage.Source = question.QuestionImage; // Définissez l'image de la question ici
            }
            else
            {
                MessageBox.Show($"Jeu terminé. Votre score est {score}/{questions.Count}");
                // Option pour redémarrer le jeu
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedAnswer = (sender as Button)?.Content.ToString();
            var question = questions[currentQuestionIndex];

            // Mettez à jour la progression pour la question actuelle
            double newProgressValue = ((double)currentQuestionIndex + 1) / questions.Count * 100;
            UpdateProgressBar(newProgressValue);

            if (question.IsCorrectAnswer(selectedAnswer))
            {
                score++;
            }

            currentQuestionIndex++;
            ShowQuestion();
        }

        private void UpdateProgressBar(double newValue)
        {
            // Si newValue est 0, cela signifie que le quiz est redémarré. Réinitialisez directement la valeur.
            if (newValue == 0)
            {
                progressBar.Value = 0;
            }

            DoubleAnimation animation = new DoubleAnimation
            {
                From = progressBar.Value,
                To = newValue,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }


        private void RestartButton_Click(object sender, RoutedEventArgs e)
        {
            // Réinitialiser l'index de la question actuelle et le score
            currentQuestionIndex = 0;
            score = 0;

            // Vous pouvez choisir de randomiser à nouveau les questions ici ou de les laisser dans l'ordre actuel
            questions = questions.OrderBy(q => Guid.NewGuid()).ToList();

            // Afficher la première question et réinitialiser la barre de progression
            ShowQuestion();
            UpdateProgressBar(0);
        }

    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswer { get; set; }
        public string ImagePath { get; set; }
        public ImageSource QuestionImage { get; set; } // Utilisez ImageSource ici

        public bool IsCorrectAnswer(string answer)
        {
            return Answers[CorrectAnswer] == answer;
        }
    }
}
