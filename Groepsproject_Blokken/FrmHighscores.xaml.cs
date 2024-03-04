using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmHighscores.xaml
    /// </summary>
    public partial class FrmHighscores : Window
    {
        public Player ingelogdePlayerMainWindow = new Player();
        public FrmHighscores()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.ingelogdePlayerLoginscreen = ingelogdePlayerMainWindow;
            this.Close();
            window.ShowDialog();
        }
    }
}
