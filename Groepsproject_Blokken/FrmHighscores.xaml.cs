using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmHighscores.xaml
    /// </summary>
    public partial class FrmHighscores : Window
    {
        public Player ingelogdePlayerMainWindow = new Player();


        public FrmHighscores()
        {
            InitializeComponent();
        }
        

        public List<Player> Players { get ; set; }

        //private void btnReturn_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow window = new MainWindow();
        //    window.ingelogdePlayerLoginscreen = ingelogdePlayerMainWindow;
        //    this.Close();
        //    window.ShowDialog();
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CleanList();
            ExcelWordStatic.PrintExcel(Players);
        }



        private void CleanList()
        {
            Players = DataManager.GetAllPlayers();
            Players = Players.OrderByDescending(p => p.SPHighscore).ToList();
            Players = Players.Take(10).ToList();
           

            
            int index = 1;
            foreach (var player in Players)
            {
                player.Position = index++;
            }

            lstHighscores.ItemsSource = Players;
            
            
        }

        private void sliderVolume_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sliderVolume.Opacity = 1;
            StackPanelVolume.Opacity = 1;
        }

        private void sliderVolume_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            sliderVolume.Opacity = 0.8;
            StackPanelVolume.Opacity = 0.4;
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (imgVolume != null)
            {
                if (sliderVolume.Value == 0)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon Mute.png", UriKind.Relative));
                }
                else if (sliderVolume.Value > 0 && sliderVolume.Value <= 33)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon Low.png", UriKind.Relative));
                }
                else if (sliderVolume.Value > 33 && sliderVolume.Value <= 66)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon Mid.png", UriKind.Relative));
                }
                else if (sliderVolume.Value > 66)
                {
                    imgVolume.Source = new BitmapImage(new Uri("Assets/Icon High.png", UriKind.Relative));
                }
            }
        }

        private void btnReturn_Click_1(object sender, RoutedEventArgs e)
        {
            BerndCrabbeTerug.Completed += (s, args) =>
            {
                FrmTitleScreen window = new FrmTitleScreen();
                window.ingelogdePlayerLoginscreen = ingelogdePlayerMainWindow;
                this.Close();
                window.ShowDialog();
            };
            BerndCrabbeTerug.Begin();
            StackPanelButtonsWeg2.Begin();
            BlokkenLogoTerug.Begin();
            
        }
    }
}
