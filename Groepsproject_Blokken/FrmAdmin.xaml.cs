using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

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
            lstManagers.ItemsSource = lijstManagers;
            lstManagers.DisplayMemberPath = "Name";

        }
       private List<Manager> lijstManagers = new List<Manager>();
        
        private void btnCreateManager_Click(object sender, RoutedEventArgs e) //TODO: Extra databinding voor validatie overeenkomende paswoorden (voorlopig messagebox) + labels hiden
        {
            Manager manager = new Manager();
            if (!(string.IsNullOrEmpty(txtUsername.Text) && string.IsNullOrEmpty(txtPassword.Text) && string.IsNullOrEmpty(txtConfirmPassword.Text)))
            {
                if (txtPassword.Text == txtConfirmPassword.Text)
                {
                    manager.Name = txtUsername.Text;
                    manager.Password = txtPassword.Text;
                    if (DataManager.InsertManager(manager) == true)
                    {
                        MessageBox.Show("Manager succesvol aangemaakt!", "Manager aangemaakt", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
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
