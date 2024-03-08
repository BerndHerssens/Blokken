using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmSinglePlayerQuiz.xaml
    /// </summary>
    public partial class FrmSinglePlayerQuiz : Window
    {
        private readonly ImageSource[] arrTilesImages = new ImageSource[]
           {
        new BitmapImage(new Uri("Assets/Tetris/TileEmpty.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TileCyan.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TileBlue.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TileOrange.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TileYellow.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TileGreen.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TilePurple.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/TileRed.png", UriKind.Relative))
           };
        private readonly ImageSource[] arrBlockImages = new ImageSource[]
        {
        new BitmapImage(new Uri("Assets/Tetris/Block-Empty.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-I.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-J.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-L.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-O.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-S.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-T.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/Tetris/Block-Z.png", UriKind.Relative))
        };
        bool correctAnswerClicked = false;
        bool blockPlaced = true;
        private readonly Image[,] arrImageControls;
        private GameState gameState = new GameState();
        public Player ingelogdePlayer;
        public PrimeWord gekozenPrimeword = new PrimeWord();
        //deze is voor in de game vensters
        char[] wordForDisplay = "________".ToCharArray(); //dit is wat we tonen op het scherm
        char[] versnipperdPrimeWord = new char[8]; //dit is het myPrimeWord.Primeword waar we mee gaan werken
                                                   //myPrimeword.Hint is je hint dat je kan tonen
        public MediaPlayer backgroundMusicPlayer = new MediaPlayer();

        private BrushConverter bc = new BrushConverter();
        private List<Question> tempLijstVragen = new List<Question>();
        public List<Question> finalLijstVragen = new List<Question>();
        private Random random = new Random();
        private Question nieuweVraag = new Question();
        private GameLogSP eenGame = new GameLogSP();
        private System.Timers.Timer _delayTimer;
        private int tellerTimer = 120;
        private DispatcherTimer timer;
        public List<string> gekozenVragenLijsten = new List<string>();
        string json = "";
        public FrmSinglePlayerQuiz()
        {
            InitializeComponent();
            arrImageControls = SetupGameCanvas(gameState.GameGrid);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            PlaybackMusic();
            InlezenVragen();
            RandomQuestionPicker();
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
            if (ingelogdePlayer == null)
            {
                lblHint.Content = "Demo";
                versnipperdPrimeWord = "ingekort".ToCharArray();
            }
            else
            {
                lblHint.Content = gekozenPrimeword.Hint;
                versnipperdPrimeWord = gekozenPrimeword.Primeword.ToCharArray();
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrmGametype gametype = new FrmGametype();
            this.Close();
            gametype.ShowDialog();
        }

        public void InlezenVragen()
        {
            foreach (string path in gekozenVragenLijsten)
            {
                using (StreamReader r = new StreamReader("../../Questionaires/" + path))
                {
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    tempLijstVragen.Clear();
                    json = r.ReadToEnd(); // Tekst inlezen in een string
                    tempLijstVragen = JsonSerializer.Deserialize<List<Question>>(json);

                    foreach (Question aQuestion in tempLijstVragen)
                    {
                        finalLijstVragen.Add(aQuestion);
                    }
                }
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
            int randomQuestionIndex = random.Next(0, finalLijstVragen.Count);
            nieuweVraag = finalLijstVragen[randomQuestionIndex];
            finalLijstVragen.RemoveAt(randomQuestionIndex);
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
                gameState.Score += 50;
                lblTimerEnScore.Content = "Score: " + gameState.Score.ToString() + " " + "Timer: " + tellerTimer;
                correctAnswerClicked = true;
                gameState.BlockIsPlaced = false;
                //txtScore.Text = (Convert.ToInt32(txtScore.Text) + 50).ToString();
            }
            else
            {
                button.Background = Brushes.OrangeRed;
                button.BorderThickness = new Thickness(0);
                gameState.Score -= 50;
                lblTimerEnScore.Content = "Score: " + gameState.Score.ToString() + " " + "Timer: " + tellerTimer;
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

        private async void Delay()
        {
            await GameLoop();
            _delayTimer = new System.Timers.Timer(100); // 5 seconds delay
            _delayTimer.Elapsed += (s, args) =>
            {
                Dispatcher.Invoke(async () =>
                {
                    if (gameState.BlockIsPlaced == true)
                    {
                        correctAnswerClicked = false;
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
                    }
                    await GameLoop();
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
                backgroundMusicPlayer.Volume = 0.15;
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
            tellerTimer--;
            lblTimerEnScore.Content = "Score: " + gameState.Score.ToString() + " " + "Timer: " + tellerTimer;
            if (tellerTimer <= 0)
            {
                backgroundMusicPlayer.Stop();
                MessageBox.Show("Game Over! Uw score was: " + gameState.Score, "Game Over!", MessageBoxButton.OK, MessageBoxImage.Stop);
                timer.Stop();

                if (ingelogdePlayer != null)
                {
                    eenGame = new GameLogSP();
                    eenGame.PlayerName = ingelogdePlayer.Name;
                    eenGame.Date = DateTime.Now;
                    eenGame.Score = Convert.ToInt32(gameState.Score);
                    eenGame.GameNumber = eenGame.GetHashCode();
                    if (ingelogdePlayer.SPHighscore < gameState.Score || ingelogdePlayer.SPHighscore == null) // Als de gamestate score hoger is of null (eerste keer spelen)
                    {
                        ingelogdePlayer.SPHighscore = gameState.Score;
                    }
                    //if (CheckAnswerIfPrimeWord()) //TODO: Primeword geraden -> uncommented wanneer bernd deeltje erbijstaat -> popup input + bool of methode uitvoeren die hier
                    //{
                    //    if (ingelogdePlayerSPQuiz.SPGamesWon == null)
                    //    {
                    //        ingelogdePlayerSPQuiz.SPGamesWon = 1;
                    //    }
                    //    else 
                    //    {
                    //        ingelogdePlayerSPQuiz.SPGamesWon++;
                    //    }
                    //}
                    DataManager.UpdatePlayer(ingelogdePlayer);
                    DataManager.InsertGameLogSP(eenGame);
                    MainWindow mainwindow = new MainWindow();
                    mainwindow.ingelogdePlayerLoginscreen = ingelogdePlayer;
                    this.Close();
                    mainwindow.ShowDialog();
                }
                else
                {
                    eenGame = new GameLogSP();
                    eenGame.PlayerName = "Gast";
                    eenGame.Date = DateTime.Now;
                    eenGame.Score = Convert.ToInt32(gameState.Score);
                    eenGame.GameNumber = eenGame.GetHashCode();
                    DataManager.InsertGameLogSP(eenGame);
                    this.Close();
                    System.Windows.Forms.Application.Restart();
                }
            }
            if (tellerTimer == 119 || tellerTimer == 96 || tellerTimer == 72 || tellerTimer == 48 || tellerTimer == 24 || tellerTimer == 1)
            {
                PrimeWordCuttingAndShowing();
            }
        }
        private Image[,] SetupGameCanvas(GameGrid gameGrid)
        {
            Image[,] imageControls = new Image[gameGrid.Rows, gameGrid.Columns];
            double cellsize = 55;

            for (int row = 0; row < gameGrid.Rows; row++)
            {
                for (int column = 0; column < gameGrid.Columns; column++)
                {
                    Image imageControl = new Image
                    {
                        Width = cellsize,
                        Height = cellsize
                    };
                    Canvas.SetTop(imageControl, (row - 2) * cellsize + 10);
                    Canvas.SetLeft(imageControl, column * cellsize);
                    GameCanvas.Children.Add(imageControl);
                    imageControls[row, column] = imageControl;
                }
            }
            gameState.BlockIsPlaced = true;
            return imageControls;
        }
        private void DrawGrid(GameGrid gameGrid)
        {
            for (int row = 0; row < gameGrid.Rows; ++row)
            {
                for (int column = 0; column < gameGrid.Columns; column++)
                {
                    int id = gameGrid[row, column];
                    arrImageControls[row, column].Opacity = 1;
                    arrImageControls[row, column].Source = arrTilesImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach (Position position in block.TilePositions())
            {
                arrImageControls[position.Row, position.Column].Opacity = 1;
                arrImageControls[position.Row, position.Column].Source = arrTilesImages[block.ID];
            }
        }

        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block nextBlock = blockQueue.NextBlock;
        }
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
            Draw(gameState);
        }

        private async Task GameLoop()
        {
            Draw(gameState);
            while (!gameState.GameOver && correctAnswerClicked == true && gameState.BlockIsPlaced == false)
            {
                gameState.BlockIsPlaced = false;
                int delay = 950;
                await Task.Delay(delay);
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            if (gameState.GameOver)
            {
                grdGameOver.Visibility = Visibility.Visible;
                txtFinalScore.Text = gameState.Score.ToString();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Left:
                    if (correctAnswerClicked == true)
                    {
                        gameState.MoveBlockLeft();
                    }
                    break;
                case Key.Right:
                    if (correctAnswerClicked == true)
                    {
                        gameState.MoveBlockRight();
                    }
                    break;
                case Key.Down:
                    if (correctAnswerClicked == true)
                    {
                        gameState.MoveBlockDown();
                    }
                    break;
                case Key.J:
                    if (gameState.BlockIsPlaced == true)
                    {
                        gameState.RotateBlockClockWise();
                    }
                    break;
                case Key.F:
                    if (correctAnswerClicked == true)
                    {
                        gameState.RotateBlockCounterClockwise();
                    }
                    break;
                case Key.Tab:
                    //gameState.HoldBlock();
                    break;
                case Key.Space:
                    if (correctAnswerClicked == false)
                    {
                        gameState.DropBlock();
                    }
                    break;
                case Key.P:
                    //gameState.Pause = true;
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }
        private async void btnPlayAgain_Click(object sender, RoutedEventArgs e)
        {
            gameState = new GameState();
            grdGameOver.Visibility = Visibility.Hidden;
            await GameLoop();
        }
        public void PrimeWordCuttingAndShowing()
        {
            lblPrimeword.Text = "";

            Random myRandom = new Random();
            int randomInt;
            bool reroll = true;
            randomInt = myRandom.Next(0, versnipperdPrimeWord.Length);
            while (reroll)
            {
                if (!(versnipperdPrimeWord[randomInt] == '_'))
                {
                    reroll = false;
                    wordForDisplay[randomInt] = versnipperdPrimeWord[randomInt];
                    versnipperdPrimeWord[randomInt] = '_';
                    foreach (char letter in wordForDisplay)
                    {
                        lblPrimeword.Text += letter.ToString().ToUpper() + " ";
                    }
                }
                else
                {
                    randomInt = myRandom.Next(0, 8);
                }
            }
        }

    }
}
