using System.Windows;

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
            FrmSinglePlayerQuiz windowSP = new FrmSinglePlayerQuiz();
            //windowSP.ingelogdePlayerSPQuiz = ingelogdePlayerMainwindow;
            this.Close();
            windowSP.ShowDialog();
        }

        private void btnVS_Click(object sender, RoutedEventArgs e)
        {
            FrmVersusQuizWindow windowVS = new FrmVersusQuizWindow();
            windowVS.ingelogdePlayerMainWindow = ingelogdePlayerMainWindow;
            this.Close();
            windowVS.ShowDialog();
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
