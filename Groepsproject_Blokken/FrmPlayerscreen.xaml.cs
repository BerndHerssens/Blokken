using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmPlayerscreen.xaml
    /// </summary>
    public partial class FrmPlayerscreen : Window
    {
        public FrmPlayerscreen()
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
