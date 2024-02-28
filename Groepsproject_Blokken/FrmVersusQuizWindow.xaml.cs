using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmVersusQuizWindow.xaml
    /// </summary>
    public partial class FrmVersusQuizWindow : Window
    {
        public FrmVersusQuizWindow()
        {
            InitializeComponent();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            FrmGametype gametype = new FrmGametype();
            this.Close();
            gametype.ShowDialog();
        }
    }
}
