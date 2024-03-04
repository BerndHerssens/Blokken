using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmPlayerRegister.xaml
    /// </summary>
    public partial class FrmPlayerRegister : Window
    {
        BitmapImage defaultProfile = new BitmapImage(new Uri(@"../../Profielfotos/default.jpg", UriKind.RelativeOrAbsolute));
        OpenFileDialog dialogChoosePicture = new OpenFileDialog
        {
            DefaultExt = "",
            Filter = "JPG files (*.jpg)|*.jpg|JPEG files(*.jpeg)|*.jpeg|PNG files (*.png)|*.png|All files (*.*)|*.*"
        };
        public FrmPlayerRegister()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtProfilePicturePreview.Source = defaultProfile;
        }

        private void btnSelectProfilePic_Click(object sender, RoutedEventArgs e)
        {
            if (dialogChoosePicture.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dialogChoosePicture.FileName.Contains(".jpg") || dialogChoosePicture.FileName.Contains(".jpeg") || dialogChoosePicture.FileName.Contains(".png"))
                {
                    txtProfilePictureSource.Text = dialogChoosePicture.FileName;
                    defaultProfile = new BitmapImage(new Uri(dialogChoosePicture.FileName, UriKind.RelativeOrAbsolute));
                    txtProfilePicturePreview.Source = defaultProfile;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("U heeft geen geldige afbeelding geselecteerd. Formaten jpg, jpeg en png worden ondersteund.", "Ongeldige selectie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCreatePlayer_Click(object sender, RoutedEventArgs e)
        {
            Player player = new Player();
            if (!(string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPassword.Text) && string.IsNullOrEmpty(txtConfirmPassword.Text)))
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    player.Name = txtUsername.Text;
                    player.Password = txtPassword.Text;
                    player.ProfilePicture = txtProfilePicturePreview.Source.ToString();
                    if (DataManager.InsertPlayer(player) == true)
                    {
                        System.Windows.Forms.MessageBox.Show("Speler succesvol aangemaakt!", "Speler registreren", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("De speler werd niet aangemaakt", "Speler registreren", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("De ingegeven wachtwoorden moeten overeenkomen", "Wachtwoord bevestigen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("U heeft niet alle benodigde velden (correct) ingevuld.", "Onvolledig formulier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
