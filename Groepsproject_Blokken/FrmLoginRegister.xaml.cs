using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmLoginRegister.xaml
    /// </summary>
    public partial class FrmLoginRegister : Window
    {
        public FrmLoginRegister()
        {
            InitializeComponent();
            
        }
        BrushConverter bc = new BrushConverter();
        

        private void btnAanmelden_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegistreren_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDoorgaanAlsGast_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAanmelden_MouseEnter(object sender, MouseEventArgs e)
        {
          
         
            btnAanmelden.Background = (Brush)bc.ConvertFrom("#002266");
        }

        private void btnRegistreren_MouseEnter(object sender, MouseEventArgs e)
        {
           
            btnRegistreren.Background = (Brush)bc.ConvertFrom("#002266");
        }

        private void btnDoorgaanAlsGast_MouseEnter(object sender, MouseEventArgs e)
        {
            btnDoorgaanAlsGast.Background = (Brush)bc.ConvertFrom("#002266");
            
        }

        private void btnDoorgaanAlsGast_MouseLeave(object sender, MouseEventArgs e)
        {
            btnDoorgaanAlsGast.Background = (Brush)bc.ConvertFrom("#fea702");
           
        }

        private void btnRegistreren_MouseLeave(object sender, MouseEventArgs e)
        {
            btnRegistreren.Background = (Brush)bc.ConvertFrom("#fea702");
      
        }

        private void btnAanmelden_MouseLeave(object sender, MouseEventArgs e)
        {
            btnAanmelden.Background = (Brush)bc.ConvertFrom("#fea702");
          
        }

    

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {

            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.Foreground = Brushes.Black;
            tb.GotFocus -= txtUsername_GotFocus;
        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            PasswordBox pb = (PasswordBox)sender;
            pb.Password = string.Empty;
            pb.Foreground = Brushes.Black;
            pb.GotFocus -= txtPassword_GotFocus;
        }
    }
}
