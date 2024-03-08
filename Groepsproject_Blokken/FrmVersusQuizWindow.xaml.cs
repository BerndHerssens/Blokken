using System.Windows;
using System.Windows.Media;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmVersusQuizWindow.xaml
    /// </summary>
    public partial class FrmVersusQuizWindow : Window
    {
        public Player ingelogdePlayerMainWindow = new Player();
        public FrmVersusQuizWindow()
        {
            InitializeComponent();
        }
        private BrushConverter bc = new BrushConverter();
        private void btnAntwoord1_Click(object sender, RoutedEventArgs e)
        {
            //ClickEvent();
            //CheckAnswer(btnAntwoord1);
            //Delay();
        }

        private void btnAntwoord2_Click(object sender, RoutedEventArgs e)
        {
            //ClickEvent();
            //CheckAnswer(btnAntwoord2);
            //Delay();

        }

        private void btnAntwoord3_Click(object sender, RoutedEventArgs e)
        {
            //    ClickEvent();
            //    CheckAnswer(btnAntwoord3);
            //    Delay();

        }

        private void btnAntwoord4_Click(object sender, RoutedEventArgs e)
        {
            //ClickEvent();
            //CheckAnswer(btnAntwoord4);
            //Delay();


        }

        private void btnAntwoord1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord1.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord1.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord1.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord1_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord1.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord1.BorderThickness = new Thickness(0);
        }

        private void btnAntwoord2_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord2.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord2.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord2.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord2_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord2.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord2.BorderThickness = new Thickness(0);
        }

        private void btnAntwoord3_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord3.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord3.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord3.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord3_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord3.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord3.BorderThickness = new Thickness(0);
        }

        private void btnAntwoord4_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord4.Background = System.Windows.Media.Brushes.WhiteSmoke;
            btnAntwoord4.BorderBrush = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord4.BorderThickness = new Thickness(5);
        }

        private void btnAntwoord4_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnAntwoord4.Background = (Brush)bc.ConvertFrom("#fea702");
            btnAntwoord4.BorderThickness = new Thickness(0);
        }


        //private void btnReturn_Click(object sender, RoutedEventArgs e)
        //{
        //    FrmGametype gametype = new FrmGametype();
        //    this.Close();
        //    gametype.ShowDialog();
        //}


    }
}
