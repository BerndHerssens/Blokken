using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmPlayerscreen.xaml
    /// </summary>
    public partial class FrmPlayerscreen : Window
    {
        public Player ingelogdePlayerMainWindow = new Player();
        OpenFileDialog dialogChoosePicture = new OpenFileDialog
        {
            DefaultExt = "",
            Filter = "JPG files (*.jpg)|*.jpg|JPEG files(*.jpeg)|*.jpeg|PNG files (*.png)|*.png|All files (*.*)|*.*"
        };
        BitmapImage bmp = new BitmapImage();
        string profilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "..\\..\\Profielfotos");
        string fileName = "";

        public FrmPlayerscreen()
        {
            InitializeComponent();


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SpelerGegevensInladen();
            //Memory forcen de image in te laden 
            MemoryStream ms = new MemoryStream();
            FileStream stream = new FileStream(ingelogdePlayerMainWindow.ProfilePicture, FileMode.Open, FileAccess.Read);
            ms.SetLength(stream.Length);
            stream.Read(ms.GetBuffer(), 0, (int)stream.Length);
            ms.Flush();
            stream.Close();
            Image i = new Image();
            bmp.BeginInit();
            bmp.StreamSource = ms;
            bmp.EndInit();
            imgProfilePic.Source = bmp;
            imgProfilePic.Stretch = System.Windows.Media.Stretch.UniformToFill;
        }


        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ingelogdePlayerLoginscreen = ingelogdePlayerMainWindow;
            this.Close();
            window.ShowDialog();
        }

        private void btnWijzigWW_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnWijzigPFP_Click(object sender, RoutedEventArgs e)
        {
            if (dialogChoosePicture.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dialogChoosePicture.FileName.Contains(".jpg") || dialogChoosePicture.FileName.Contains(".jpeg") || dialogChoosePicture.FileName.Contains(".png"))
                {
                    ingelogdePlayerMainWindow.ProfilePicture = dialogChoosePicture.FileName;
                    imgProfilePic.Source = new BitmapImage(new Uri(dialogChoosePicture.FileName, UriKind.RelativeOrAbsolute));
                    fileName = Path.GetFileName(dialogChoosePicture.FileName);
                    //Kopieren van de nieuwe pfp + gelijkstellen
                    File.Copy(dialogChoosePicture.FileName, @"../../Profielfotos/" + fileName, true);
                    ingelogdePlayerMainWindow.ProfilePicture = @"../../Profielfotos/" + fileName;
                    DataManager.UpdatePlayer(ingelogdePlayerMainWindow);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("U heeft geen geldige afbeelding geselecteerd. Formaten jpg, jpeg en png worden ondersteund.", "Ongeldige selectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnShare_Click(object sender, RoutedEventArgs e)
        {
            //Todo met tycho
        }


        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            // YesButton Clicked -> Hide Inputvenster
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            //Verwerk data
            String input = InputTextBox.Text;
            ingelogdePlayerMainWindow.Password = InputTextBox.Text;
            DataManager.UpdatePlayer(ingelogdePlayerMainWindow); //Todo: Bevestiging in de datamanager?

            //Reset
            InputTextBox.Text = String.Empty;
        }
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            // NoButton Clicked -> Hide box
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            //Reset
            InputTextBox.Text = String.Empty;
        }
        private void SpelerGegevensInladen()
        {
            lblNaamDisp.Content = ingelogdePlayerMainWindow.Name;
            lblGamesGespeeldSPDisp.Content = "0";
            lblGamesGespeeldVSDisp.Content = "0";
            lblGamesGespeeldSPDisp.Content = "0";
            lblGamesGespeeldVSDisp.Content = "0";
            if (ingelogdePlayerMainWindow.SPGamesPlayed != null)
            {
                lblGamesGespeeldSPDisp.Content = ingelogdePlayerMainWindow.SPGamesPlayed;
            }

            if (ingelogdePlayerMainWindow.VSGamesPlayed != null)
            {
                lblGamesGespeeldVSDisp.Content = ingelogdePlayerMainWindow.VSGamesPlayed;
            }

            if (ingelogdePlayerMainWindow.SPHighscore != null)
            {
                lblHighscoreSPDisp.Content = ingelogdePlayerMainWindow.SPHighscore;

            }
            if (ingelogdePlayerMainWindow.VSHighscore != null)
            {
                lblHighscoreVSDisp.Content = ingelogdePlayerMainWindow.VSHighscore;
            }

            lblWinrateSPDisp.Content = ingelogdePlayerMainWindow.CalculateSPWinRate();
            lblWinrateVSDisp.Content = ingelogdePlayerMainWindow.CalculateVSWinRate();

        }
    }
}
