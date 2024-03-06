using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player ingelogdePlayerLoginscreen = new Player();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            StackPanelButtonsWeg.Completed += (s, args) =>
            {
                FrmGametype chooseGame = new FrmGametype();
                chooseGame.ingelogdePlayerMainWindow = ingelogdePlayerLoginscreen;
                this.Close();
                chooseGame.ShowDialog();
            };

            StackPanelButtonsWeg.Begin();
        }

        private void btnHighscore_Click(object sender, RoutedEventArgs e)
        {
            FrmHighscores frmHighscores = new FrmHighscores();
            frmHighscores.ingelogdePlayerMainWindow = ingelogdePlayerLoginscreen;
            this.Close();
            frmHighscores.ShowDialog();

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrmPlayerscreen frmPlayerscreen = new FrmPlayerscreen();
            frmPlayerscreen.ingelogdePlayerMainWindow = ingelogdePlayerLoginscreen;
            this.Close();
            frmPlayerscreen.ShowDialog();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        private void sliderVolume_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sliderVolume.Opacity = 1;
            StackPanelVolume.Opacity = 1;
        }

        private void sliderVolume_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sliderVolume.Opacity = 0.8;
            StackPanelVolume.Opacity = 0.4;
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (imgVolume != null)
            {
                if (sliderVolume.Value == 0)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon Mute.png", UriKind.Relative));
                }
                else if (sliderVolume.Value > 0 && sliderVolume.Value <= 33)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon Low.png", UriKind.Relative));
                }
                else if (sliderVolume.Value > 33 && sliderVolume.Value <= 66)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon Mid.png", UriKind.Relative));
                }
                else if (sliderVolume.Value > 66)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon High.png", UriKind.Relative));
                }
            }
        }
    }


}


