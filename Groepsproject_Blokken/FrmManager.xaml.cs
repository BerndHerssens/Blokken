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

        public void WegSchrijven()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IncludeFields = false;
            options.WriteIndented = true;
            string json = JsonSerializer.Serialize(tempquestions, options);
            File.WriteAllText("../../Questionaires/VragenJson", json);
        }
        public void InlezenVragen()
        {
            using (StreamReader r = new StreamReader("../../Questionaires/VragenJson"))
            {
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.IncludeFields = false;
                tempquestions.Clear();            // Lijst leegmaken
                string json = r.ReadToEnd(); // Tekst inlezen in een string
                tempquestions = JsonSerializer.Deserialize<List<Question>>(json); //De string inlezen en deserializen in een list, de properties moeten overeen komen.
            }
        }
    }
}
