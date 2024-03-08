using System.Collections.Generic;
using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmAdmin.xaml
    /// </summary>
    public partial class FrmAdmin : Window
    {
        public FrmAdmin()
        {
            InitializeComponent();
            lijstManagers = DataManager.GetAllManagers();
            lstPlayers = DataManager.GetAllPlayers();
            lstManagers.ItemsSource = lijstManagers;
            lstManagers.DisplayMemberPath = "Name";

        }
        private List<Manager> lijstManagers = new List<Manager>();
        private List<Player> lstPlayers = new List<Player>();
        bool managerGevonden = false;

        private void btnCreateManager_Click(object sender, RoutedEventArgs e) //TODO: Extra databinding voor validatie overeenkomende paswoorden (voorlopig messagebox) + labels hiden
        {
            bool managerGevonden = false;
            Manager manager = new Manager();
            if (!(string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPassword.Text) && string.IsNullOrEmpty(txtConfirmPassword.Text)))
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    manager.Name = txtUsername.Text;
                    foreach (Manager gelogdeManager in lijstManagers)
                    {
                        if (gelogdeManager.Equals(manager))
                        {
                            managerGevonden = true;
                        }
                    }
                    foreach (Player gelogdePLayer in lstPlayers)
                    {
                        if (gelogdePLayer.Name == manager.Name)
                        {
                            managerGevonden = true;
                        }
                    }
                    if (managerGevonden == true)
                    {
                        MessageBox.Show("Deze gebruikersnaam is al in gebruik, kies een andere gebruikersnaam", "Speler registreren", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    else
                    {
                        if (DataManager.InsertManager(manager) == true)
                        {
                            MessageBox.Show("Manager succesvol aangemaakt!", "Manager aangemaakt", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    manager.Password = txtPassword.Text;

                }
                else
                {
                    MessageBox.Show("Passwoord fout", "Passwoorden komen niet overeen.", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
