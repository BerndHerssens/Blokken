using System.Windows;


namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmQuizWindow.xaml
    /// </summary>
    public partial class FrmQuizWindow : Window
    {

        public FrmQuizWindow()
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
