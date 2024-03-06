using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmGametype.xaml
    /// </summary>
    public partial class FrmGametype : Window
    {
        public Player ingelogdePlayerMainWindow = new Player();
        public FrmGametype()
        {
            InitializeComponent();
        }

        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            
            BerndCrabbeWeg.Completed += (s, args) =>
            {
                FrmSinglePlayerQuiz windowSP = new FrmSinglePlayerQuiz();
                windowSP.ingelogdePlayerSPQuiz = ingelogdePlayerMainWindow;
                this.Close();
                windowSP.ShowDialog();
            };

            BerndCrabbeWeg.Begin();

        }

        private void btnVS_Click(object sender, RoutedEventArgs e)
        {
            StackPanelButtonsWeg.Completed += (s, args) =>
            {
                FrmVersusQuizWindow windowVS = new FrmVersusQuizWindow();
                windowVS.ingelogdePlayerMainWindow = ingelogdePlayerMainWindow;
                this.Close();
                windowVS.ShowDialog();
            };
            StackPanelButtonsWeg.Begin();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            
            StackPanelButtonsWeg.Completed += (s, args) =>
            {
                MainWindow window = new MainWindow();
                window.ingelogdePlayerLoginscreen = ingelogdePlayerMainWindow;
                this.Close();
                window.ShowDialog();
            };
            StackPanelButtonsWeg.Begin();
            


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

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
