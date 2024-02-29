using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
//Todo: Met front end de lijst displayen en zorgen dat we deze kunnen crudden
namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmManager.xaml
    /// </summary>
    public partial class FrmManager : Window
    {
        public FrmManager()
        {
            InitializeComponent();
        }
        List<Question> tempquestions = new List<Question>(); //Opslagen van questions om later te editten
        Question question1;


        public void WegSchrijven(List<Question> eenList, string fileName)
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(eenList, options);
            File.WriteAllText("../../Questionaires/" + fileName, json);
        }
        public void InlezenVragen()
        {
            using (StreamReader r = new StreamReader("../../Questionaires/VragenJson"))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                tempquestions.Clear();            // Lijst leegmaken
                string json = r.ReadToEnd(); // Tekst inlezen in een string
                tempquestions = JsonSerializer.Deserialize<List<Question>>(json); //De string inlezen en deserializen in een list, de properties moeten overeen komen.
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
