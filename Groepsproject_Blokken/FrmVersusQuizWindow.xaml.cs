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
    /// Interaction logic for FrmVersusQuizWindow.xaml
    /// </summary>
    public partial class FrmVersusQuizWindow : Window
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
        bool buzzerPressed = false;
        bool buzzerPlayer1 = false;
        bool buzzerPlayer2 = false;
        private readonly Image[,] arrImageControls;
        private  GameState gameState = new GameState();
        public Player ingelogdePlayer1;
        public Player ingelogdePlayer2;
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
        public Player ingelogdePlayerMainWindow = new Player();
        public FrmVersusQuizWindow()
        {
            InitializeComponent();
            arrImageControls = SetupGameCanvas(gameState.GameGrid);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InlezenVragen();
            ProfilePicturesInladen();
            RandomQuestionPicker();
            EnableDisableAnswers();
            //TODO: profielfotos inladen spelers
            timer = new DispatcherTimer(DispatcherPriority.Render);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        public void ProfilePicturesInladen()
        {
            ingelogdePlayer1.ImageInladenMetMemoryStream();
            ingelogdePlayer2.ImageInladenMetMemoryStream();
            imgSpeler1.Source = ingelogdePlayer1.BMP;
            imgSpeler2.Source = ingelogdePlayer2.BMP;
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
        private void RandomQuestionPicker()
        {
            int randomQuestionIndex = random.Next(0, finalLijstVragen.Count);
            nieuweVraag = finalLijstVragen[randomQuestionIndex];
            finalLijstVragen.RemoveAt(randomQuestionIndex);
            lblVraag.Content = nieuweVraag.TheQuestion;
            lblVraag2.Content = nieuweVraag.TheQuestion;
            nieuweVraag.InsertAnswersInButtons(btnAntwoord1, btnAntwoord2, btnAntwoord3, btnAntwoord4);
            btnAntwoord1.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord2.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord3.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord4.Background = (Brush)bc.ConvertFrom("#fea702");
        }
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
                if (buzzerPlayer1 == true)
                {
                    gameState.ScorePlayerOne += 50;
                }
                if (buzzerPlayer2 == true)
                {
                    gameState.ScorePlayerTwo += 50;
                }
                button.Background = Brushes.Green;
                button.BorderThickness = new Thickness(0);
                lblScoreSpeler1.Content = "Score P1: " + gameState.ScorePlayerOne.ToString();
                lblScoreSpeler2.Content = "Score P2: " + gameState.ScorePlayerTwo.ToString();
                correctAnswerClicked = true;
                gameState.BlockIsPlaced = false;
                //txtScore.Text = (Convert.ToInt32(txtScore.Text) + 50).ToString();
            }
            else
            {
                if (buzzerPlayer1 == true)
                {
                    gameState.ScorePlayerOne -= 50;
                }
                if (buzzerPlayer2 == true)
                {
                    gameState.ScorePlayerTwo -= 50;
                }
                button.Background = Brushes.OrangeRed;
                button.BorderThickness = new Thickness(0);
                lblScoreSpeler1.Content = "Score P1: " + gameState.ScorePlayerOne.ToString();
                lblScoreSpeler2.Content = "Score P2: " + gameState.ScorePlayerTwo.ToString();

                ShowCorrectAnswer(new List<Button> { btnAntwoord1, btnAntwoord2, btnAntwoord3, btnAntwoord4 });
            }
            Overlay.Visibility = Visibility.Collapsed;
        }

        public void EnableDisableAnswers()
        {
            switch (buzzerPressed)
            {
                case (true):
                    btnAntwoord1.IsEnabled = true;
                    btnAntwoord2.IsEnabled = true;
                    btnAntwoord3.IsEnabled = true;
                    btnAntwoord4.IsEnabled = true;
                    break;
                case (false):
                    btnAntwoord1.IsEnabled = false;
                    btnAntwoord2.IsEnabled = false;
                    btnAntwoord3.IsEnabled = false;
                    btnAntwoord4.IsEnabled = false;
                    break;
                default:
                    break;
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
                        buzzerPressed = false;
                        buzzerPlayer1 = false;
                        buzzerPlayer2 = false;
                        EnableDisableAnswers();
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

        private void Media_Ended(object sender, EventArgs e)
        {

        }

        void timer_Tick(object sender, EventArgs e)
        {
            tellerTimer--;
            lblScoreSpeler1.Content = "Score P1: " + gameState.ScorePlayerOne.ToString();
            lblScoreSpeler2.Content = "Score P2: " + gameState.ScorePlayerTwo.ToString();
            if (finalLijstVragen.Count == 0) 
            { 
                //einde spel komt hier
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
            gameState.RowIsCleared = false;
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
                if (gameState.RowIsCleared == true)
                {
                    if (buzzerPlayer1 == true)
                    {
                        gameState.ScorePlayerOne += gameState.Score;
                    }
                    else if (buzzerPlayer2 == true)
                    {
                        gameState.ScorePlayerTwo += gameState.Score;
                    }
                    gameState.Score = 0;
                }
                Draw(gameState);
            }
            //if (gameState.GameOver)
            //{
            //    grdGameOver.Visibility = Visibility.Visible;
            //    txtFinalScore.Text = gameState.Score.ToString();
            //}
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.GameOver)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Q:
                    if (correctAnswerClicked == true && buzzerPlayer1 == true)
                    {
                        gameState.MoveBlockLeft();
                    }
                    break;
                case Key.D:
                    if (correctAnswerClicked == true && buzzerPlayer1 == true)
                    {
                        gameState.MoveBlockRight();
                    }
                    break;
                case Key.X:
                    if (correctAnswerClicked == true && buzzerPlayer1 == true)
                    {
                        gameState.MoveBlockDown();
                    }
                    break;
                case Key.A:
                    if (gameState.BlockIsPlaced == true && buzzerPlayer1 == true)
                    {
                        gameState.RotateBlockClockWise();
                    }
                    break;
                case Key.E:
                    if (correctAnswerClicked == true && buzzerPlayer1 == true)
                    {
                        gameState.RotateBlockCounterClockwise();
                    }
                    break;
                case Key.Z:
                    if (buzzerPressed == false)
                    {
                        buzzerPressed = true;
                        buzzerPlayer1 = true;
                        Overlay.Visibility = Visibility.Visible;
                        EnableDisableAnswers();
                    }
                    break;
                case Key.NumPad4:
                    if (correctAnswerClicked == true && buzzerPlayer2 == true)
                    {
                        gameState.MoveBlockLeft();
                    }
                    break;
                case Key.NumPad6:
                    if (correctAnswerClicked == true && buzzerPlayer2 == true)
                    {
                        gameState.MoveBlockRight();
                    }
                    break;
                case Key.NumPad2:
                    if (correctAnswerClicked == true && buzzerPlayer2 == true)
                    {
                        gameState.MoveBlockDown();
                    }
                    break;
                case Key.NumPad7:
                    if (gameState.BlockIsPlaced == true && buzzerPlayer2 == true)
                    {
                        gameState.RotateBlockClockWise();
                    }
                    break;
                case Key.NumPad9:
                    if (correctAnswerClicked == true && buzzerPlayer2 == true)
                    {
                        gameState.RotateBlockCounterClockwise();
                    }
                    break;
                case Key.NumPad8:
                    if (buzzerPressed == false)
                    {
                        buzzerPressed = true;
                        buzzerPlayer2 = true;
                        Overlay.Visibility = Visibility.Visible;
                        EnableDisableAnswers();
                    }
                    break;
                case Key.Tab:
                    //gameState.HoldBlock();
                    break;
                case Key.Space:
                    if (correctAnswerClicked == false)
                    {
                        //gameState.DropBlock();
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
        //private void btnReturn_Click(object sender, RoutedEventArgs e)
        //{
        //    FrmGametype gametype = new FrmGametype();
        //    this.Close();
        //    gametype.ShowDialog();
        //}

    }
}
