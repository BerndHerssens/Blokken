using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmGametype.xaml
    /// </summary>
    public partial class FrmGametype : Window
    {
        public FrmGametype()
        {
            InitializeComponent();
        }

        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            FrmQuizWindow windowSP = new FrmQuizWindow();
            this.Close();
            windowSP.ShowDialog();
        }

        private void btnVS_Click(object sender, RoutedEventArgs e)
        {
            FrmVersusQuizWindow windowVS = new FrmVersusQuizWindow();
            this.Close();
            windowVS.ShowDialog();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            this.Close();
            window.ShowDialog();
        }
    }
}
