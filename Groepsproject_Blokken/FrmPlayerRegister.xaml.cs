using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmPlayerRegister.xaml
    /// </summary>
    public partial class FrmPlayerRegister : Window
    {

        public List<Player> lstPlayers = new List<Player>();
        public List<Manager> lstManagers = new List<Manager>();
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
            txtProfilePictureSource.Text = defaultProfile.UriSource.ToString();
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
            bool playerBestaat = false;
            Player player = new Player();
            if (!(string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPassword.Text) && string.IsNullOrEmpty(txtConfirmPassword.Text)))
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    player.Name = txtUsername.Text;
                    foreach (Player loggedPlayer in lstPlayers)
                    {
                        if (loggedPlayer.Equals(player))
                        {
                            playerBestaat = true;
                            System.Windows.Forms.MessageBox.Show("Deze gebruikersnaam is al in gebruik, kies een andere gebruikersnaam", "Speler registreren", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        foreach (Manager loggedManager in lstManagers)
                        {
                            if (loggedManager.Name == player.Name)
                            {
                                playerBestaat = true;
                                System.Windows.Forms.MessageBox.Show("Deze gebruikersnaam is al in gebruik, kies een andere gebruikersnaam", "Speler registreren", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    if (playerBestaat == false)
                    {
                        player.Password = txtPassword.Text;
                        player.ProfilePicture = txtProfilePicturePreview.Source.ToString();
                        if (DataManager.InsertPlayer(player) == true)
                        {
                            System.Windows.Forms.MessageBox.Show("Speler succesvol aangemaakt!", "Speler registreren", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            lstPlayers.Add(player);
                            this.DialogResult = true;
                            return;
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("De speler werd niet aangemaakt", "Speler registreren", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
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
