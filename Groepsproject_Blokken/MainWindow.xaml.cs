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
            this.Close();
            chooseGame.ShowDialog();
        }

        private void btnHighscore_Click(object sender, RoutedEventArgs e)
        {
            FrmHighscores frmHighscores = new FrmHighscores();
            this.Close();
            frmHighscores.ShowDialog();

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrmPlayerscreen frmPlayerscreen = new FrmPlayerscreen();
            this.Close();
            frmPlayerscreen.ShowDialog();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Nog niks , weet nog niet 100% zeker waar we terug gaan
        }
    }


}


