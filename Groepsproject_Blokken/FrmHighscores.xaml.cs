using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmHighscores.xaml
    /// </summary>
    public partial class FrmHighscores : Window
    {
        public FrmHighscores()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }
    }
}
