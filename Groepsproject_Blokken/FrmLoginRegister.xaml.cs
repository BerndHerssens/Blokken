using System.Collections.Generic;
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
        List<Player> playerList = new List<Player>();
        List<Manager> managerList = new List<Manager>();
        List<Admin> adminList = new List<Admin>();
        Player ingelogdePlayer = new Player();
        Manager ingelogdeManager = new Manager();
        Admin ingelogdeAdmin = new Admin();
        bool adminGevonden = false;
        bool managerGevonden = false;
        bool playerGevonden = false;
        //TIJDELIJKE INLOGS/FAILSAFES DIE HARDCODED STAAN, DEZE ZOUDE WEG MOGEN LATER OF WE KUNNEN ZE LATEN STAAN INCASE IETS BREEKT
        Admin tempAdmin = new Admin();
        Manager tempManager = new Manager();
        Player tempPlayer = new Player();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TIJDELIJKE INLOGS/FAILSAFES DIE HARDCODED STAAN, DEZE ZOUDE WEG MOGEN LATER OF WE KUNNEN ZE LATEN STAAN INCASE IETS BREEKT
            tempAdmin.Name = "tAdmin";
            tempAdmin.Password = "tAdmin";
            tempManager.Name = "tManager";
            tempManager.Password = "tManager";
            tempPlayer.Name = "tPlayer";
            tempPlayer.Password = "tPlayer";
            //Inladen van db/json 

            playerList = DataManager.GetAllPlayers();
            managerList = DataManager.GetAllManagers();
            adminList = DataManager.GetAllAdmins();

        }
        private void btnAanmelden_Click(object sender, RoutedEventArgs e)
        {
            //inlogsysteem voor failsafe/hardcoded tempAdmin/Manager/Speler (TIJDELIJK)
            LoginAlsTempGebruiker();

            //eerste attempt om een login systeem te maken voor met json/db
            VindIngelogdeGebruiker();
        }


        private void btnRegistreren_Click(object sender, RoutedEventArgs e)
        {
            FrmPlayerRegister frmPlayerRegister = new FrmPlayerRegister();
            this.Hide();
            frmPlayerRegister.ShowDialog();
        }

        private void btnDoorgaanAlsGast_Click(object sender, RoutedEventArgs e)
        {
            FrmSinglePlayerQuiz frmQuizWindow = new FrmSinglePlayerQuiz();
            frmQuizWindow.ShowDialog();

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
        //Methode IDEE voor inte loggen
        private void VindIngelogdeGebruiker()
        {
            //Ik heb 3 bools toegevoegd om niet altijd de iteratie moeten uit te voer
            if (adminGevonden == false)
            {
                foreach (Admin admin in adminList)
                {
                    if (admin.Name == txtUsername.Text && admin.Password == txtPassword.Password)
                    {
                        ingelogdeAdmin = DataManager.GetLoggedInAdmin(txtUsername.Text, txtPassword.Password);
                        adminGevonden = true;
                        managerGevonden = true;
                        playerGevonden = true;
                        FrmAdmin frmAdmin = new FrmAdmin();
                        this.Close();
                        frmAdmin.ShowDialog();
                    }
                }
            }
            if (managerGevonden == false)
            {
                foreach (Manager manager in managerList)
                {
                    if (manager.Name == txtUsername.Text && manager.Password == txtPassword.Password)
                    {
                        ingelogdeManager = DataManager.GetLoggedInManager(txtUsername.Text, txtPassword.Password);
                        managerGevonden = true;
                        playerGevonden = true;
                        FrmManager frmManager = new FrmManager();
                        this.Close();
                        frmManager.ShowDialog();
                    }
                }
            }
            if (playerGevonden == false)
            {
                foreach (Player player in playerList)
                {
                    if (player.Name == txtUsername.Text && player.Password == txtPassword.Password)
                    {
                        ingelogdePlayer = DataManager.GetLoggedInPlayer(txtUsername.Text, txtPassword.Password);
                        playerGevonden = true;
                        MainWindow mainwindow = new MainWindow();
                        this.Close();
                        mainwindow.ShowDialog();
                    }
                }
            }
            //reset bools voor volgende login
            adminGevonden = false;
            managerGevonden = false;
            playerGevonden = false;
        }
        //(TIJDELIJKE) Methode voor te kunnen inloggen zonder json gedoe
        private void LoginAlsTempGebruiker()
        {
            if (tempAdmin.Name == txtUsername.Text && tempAdmin.Password == txtPassword.Password)
            {
                FrmAdmin frmAdmin = new FrmAdmin();
                this.Close();
                frmAdmin.ShowDialog();
            }
            else if (tempManager.Name == txtUsername.Text && tempManager.Password == txtPassword.Password)
            {
                FrmManager frmManager = new FrmManager();
                this.Close();
                frmManager.ShowDialog();
            }
            else if (tempPlayer.Name == txtUsername.Text && tempPlayer.Password == txtPassword.Password)
            {
                MainWindow mainwindow = new MainWindow();
                this.Close();
                mainwindow.ShowDialog();
            }

        }


    }
}
