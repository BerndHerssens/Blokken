using System;
using System.Windows;
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
        public FrmPlayerscreen()
        {
            InitializeComponent();


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SpelerGegevensInladen();
        }


        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
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
                    txtProfilePicturePreview.Source = new BitmapImage(new Uri(dialogChoosePicture.FileName, UriKind.RelativeOrAbsolute));
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
            //Todo: profielfoto
            txtProfilePicturePreview.Source = new BitmapImage(new Uri(ingelogdePlayerMainWindow.ProfilePicture, UriKind.RelativeOrAbsolute));
        }
    }
}
