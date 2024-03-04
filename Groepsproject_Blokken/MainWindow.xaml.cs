﻿using System.Windows;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Player ingelogdePlayerLoginscreen = new Player();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            FrmGametype chooseGame = new FrmGametype();
            chooseGame.ingelogdePlayerMainWindow = ingelogdePlayerLoginscreen;
            this.Close();
            chooseGame.ShowDialog();
        }

        private void btnHighscore_Click(object sender, RoutedEventArgs e)
        {
            FrmHighscores frmHighscores = new FrmHighscores();
            frmHighscores.ingelogdePlayerMainWindow = ingelogdePlayerLoginscreen;
            this.Close();
            frmHighscores.ShowDialog();

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            FrmPlayerscreen frmPlayerscreen = new FrmPlayerscreen();
            frmPlayerscreen.ingelogdePlayerMainWindow = ingelogdePlayerLoginscreen;
            this.Close();
            frmPlayerscreen.ShowDialog();
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            System.Windows.Application.Current.Shutdown();
        }
    }


}


