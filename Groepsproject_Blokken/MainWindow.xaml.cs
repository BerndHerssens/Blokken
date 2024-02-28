using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }



        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            FrmGametype chooseGame = new FrmGametype();
            this.Hide();
            chooseGame.ShowDialog();

        }

        private void btnHighscore_Click(object sender, RoutedEventArgs e)
        {
            FrmHighscores frmHighscores = new FrmHighscores();
            this.Hide();
            frmHighscores.ShowDialog();

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrmPlayerscreen frmPlayerscreen = new FrmPlayerscreen();
            this.Hide();
            frmPlayerscreen.ShowDialog();

        }
    }

}
