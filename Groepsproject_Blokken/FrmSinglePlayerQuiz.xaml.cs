using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmSinglePlayerQuiz.xaml
    /// </summary>
    public partial class FrmSinglePlayerQuiz : Window
    {
        public FrmSinglePlayerQuiz()
        {
            InitializeComponent();
            PlaybackMusic();
            InlezenVragen();
            RandomQuestionPicker();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        public MediaPlayer backgroundMusicPlayer = new MediaPlayer();
        
        private BrushConverter bc = new BrushConverter();
        private List<Question> lijstVragen = new List<Question>();
        private Random random = new Random();
        private Question nieuweVraag = new Question();

        private System.Timers.Timer _delayTimer;
        private int teller = 120;
        private DispatcherTimer timer;

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrmGametype gametype = new FrmGametype();
            this.Close();
            gametype.ShowDialog();
        }

        public void InlezenVragen()
        {
            using (StreamReader r = new StreamReader("../../Questionaires/Actua2023"))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                lijstVragen.Clear();            // Lijst leegmaken
                string json = r.ReadToEnd(); // Tekst inlezen in een string
                lijstVragen = JsonSerializer.Deserialize<List<Question>>(json);
            }
        }
        private void btnAntwoord1_Click(object sender, RoutedEventArgs e)
        {
            ClickEvent();
            CheckAnswer(btnAntwoord1);
            Delay();
        }

        private void btnAntwoord2_Click(object sender, RoutedEventArgs e)
        {
            ClickEvent();
            CheckAnswer(btnAntwoord2);
            Delay();

        }

        private void btnAntwoord3_Click(object sender, RoutedEventArgs e)
        {
            ClickEvent();
            CheckAnswer(btnAntwoord3);
            Delay();

        }

        private void btnAntwoord4_Click(object sender, RoutedEventArgs e)
        {
            ClickEvent();
            CheckAnswer(btnAntwoord4);
            Delay();


        }

        private void btnAntwoord1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord1.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord1.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord1.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord1.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord1.BorderThickness = new Thickness(0);
        }

        private void btnAntwoord2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord2.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord2.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord2.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord2.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord2.BorderThickness = new Thickness(0);
        }

        private void btnAntwoord3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord3.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord3.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord3.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord3.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord3.BorderThickness = new Thickness(0);
        }

        private void btnAntwoord4_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord4.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord4.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord4.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord4_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord4.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord4.BorderThickness = new Thickness(0);
        }

        private void RandomQuestionPicker()
        {
            int randomQuestionIndex = random.Next(0, lijstVragen.Count);
            nieuweVraag = lijstVragen[randomQuestionIndex];
            lijstVragen.RemoveAt(randomQuestionIndex);
            txtVraag.Text = nieuweVraag.TheQuestion;
            nieuweVraag.InsertAnswersInButtons(btnAntwoord1, btnAntwoord2, btnAntwoord3, btnAntwoord4);
            btnAntwoord1.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord2.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord3.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord4.Background = (Brush)bc.ConvertFrom("#fea702");


        }

        //Looped door de buttons en laat het correcte antwoord zien (gebruiken wanneer niemand het juist heeft)
        public void ShowCorrectAnswer(List<Button> lijstButtons)
        {
            foreach (Button button in lijstButtons)
            {
                if ((string)button.Content == nieuweVraag.CorrectAnswer)
                {
                    button.Background = Brushes.Green;
                }
            }
        }
        private void CheckAnswer(Button button)
        {
            if ((string)button.Content == nieuweVraag.CorrectAnswer)
            {
                button.Background = Brushes.Green;
                button.BorderThickness = new Thickness(0);
                //txtScore.Text = (Convert.ToInt32(txtScore.Text) + 50).ToString();
            }
            else
            {
                button.Background = Brushes.OrangeRed;
                button.BorderThickness = new Thickness(0);
                //txtScore.Text = (Convert.ToInt32(txtScore.Text) - 50).ToString();

                ShowCorrectAnswer(new List<Button> { btnAntwoord1, btnAntwoord2, btnAntwoord3, btnAntwoord4 });
            }
        }

        private void ClickEvent()
        {
            btnAntwoord1.MouseEnter -= btnAntwoord1_MouseEnter;
            btnAntwoord1.MouseLeave -= btnAntwoord1_MouseLeave;
            btnAntwoord1.Click -= btnAntwoord1_Click;
            btnAntwoord2.MouseEnter -= btnAntwoord2_MouseEnter;
            btnAntwoord2.MouseLeave -= btnAntwoord2_MouseLeave;
            btnAntwoord2.Click -= btnAntwoord2_Click;
            btnAntwoord3.MouseEnter -= btnAntwoord3_MouseEnter;
            btnAntwoord3.MouseLeave -= btnAntwoord3_MouseLeave;
            btnAntwoord3.Click -= btnAntwoord3_Click;
            btnAntwoord4.MouseEnter -= btnAntwoord4_MouseEnter;
            btnAntwoord4.MouseLeave -= btnAntwoord4_MouseLeave;
            btnAntwoord4.Click -= btnAntwoord4_Click;
        }

        private void Delay()
        {
            _delayTimer = new System.Timers.Timer(5000); // 5 seconds delay
            _delayTimer.Elapsed += (s, args) =>
            {
                Dispatcher.Invoke(() =>
                {
                    RandomQuestionPicker();
                    btnAntwoord1.MouseEnter += btnAntwoord1_MouseEnter;
                    btnAntwoord1.MouseLeave += btnAntwoord1_MouseLeave;
                    btnAntwoord1.Click += btnAntwoord1_Click;
                    btnAntwoord2.MouseEnter += btnAntwoord2_MouseEnter;
                    btnAntwoord2.MouseLeave += btnAntwoord2_MouseLeave;
                    btnAntwoord2.Click += btnAntwoord2_Click;
                    btnAntwoord3.MouseEnter += btnAntwoord3_MouseEnter;
                    btnAntwoord3.MouseLeave += btnAntwoord3_MouseLeave;
                    btnAntwoord3.Click += btnAntwoord3_Click;
                    btnAntwoord4.MouseEnter += btnAntwoord4_MouseEnter;
                    btnAntwoord4.MouseLeave += btnAntwoord4_MouseLeave;
                    btnAntwoord4.Click += btnAntwoord4_Click;

                });
            };
            _delayTimer.AutoReset = false;
            _delayTimer.Start();
        }
        public void PlaybackMusic()
        {
            string backgroundMusicFilePath = "../../Assets/Blokken PlaceHolderLoop.wav";
            if (!string.IsNullOrEmpty(backgroundMusicFilePath))
            {
                backgroundMusicPlayer.Open(new Uri(backgroundMusicFilePath, UriKind.Relative));
                backgroundMusicPlayer.MediaEnded += new EventHandler(Media_Ended);
                backgroundMusicPlayer.Play();
            }
        }
        private void Media_Ended(object sender, EventArgs e)
        {
            backgroundMusicPlayer.Position = TimeSpan.Zero;
            backgroundMusicPlayer.Play();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            //teller--;
            //txtTimer.Text = teller.ToString();
            //if (teller < 0)
            //{
            //    GameLogSP eenGame = new GameLogSP();
            //    eenGame.PlayerName = "Gast"; // TODO : We moeten de speler overdragen
            //    eenGame.Date = DateTime.Now;
            //    eenGame.Score = Convert.ToInt32(txtScore.Text);
            //    eenGame.GameNumber = eenGame.GetHashCode();
            //    DataManager.InsertGameLogSP(eenGame);

            //}
        }
    }  
}
