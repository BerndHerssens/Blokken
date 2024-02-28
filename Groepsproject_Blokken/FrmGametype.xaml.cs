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
            FrmQuizWindow WindowSP = new FrmQuizWindow();
            this.Hide();
            WindowSP.ShowDialog();


        }

        private void btnVS_Click(object sender, RoutedEventArgs e)
        {
            FrmVersusQuizWindow windowVS = new FrmVersusQuizWindow();
            this.Hide();
            windowVS.ShowDialog();

        }
    }
}
