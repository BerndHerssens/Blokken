using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//Todo: Met front end de lijst displayen en zorgen dat we deze kunnen crudden
namespace Groepsproject_Blokken
{
    /// <summary>
    /// Interaction logic for FrmManager.xaml
    /// </summary>
    public partial class FrmManager : Window
    {
        List<Question> tempquestions = new List<Question>(); //Opslagen van questions om later te editten
        Question question;
        public FrmManager()
        {
            InitializeComponent();
        }

        private void btnSaveList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtFileName.Text) && tempquestions.Count != 0)
                {
                    WegSchrijven(tempquestions, txtFileName.Text);
                    tempquestions.Clear();
                    RefreshFields();
                    MessageBox.Show("Vragenlijst succesvol opgeslagen!", "Vragen opgeslagen", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            } catch 
            { 
                MessageBox.Show("Er ging iets mis. Het bestand is niet opgeslagen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            question = new Question();
            if (!(string.IsNullOrEmpty(txtQuestion.Text) && string.IsNullOrEmpty(txtCorrectAnswer.Text) && string.IsNullOrEmpty(txtWrongAnswer1.Text) && string.IsNullOrEmpty(txtWrongAnswer2.Text) && string.IsNullOrEmpty(txtWrongAnswer3.Text)))
            {
                question.TheQuestion = txtQuestion.Text;
                question.CorrectAnswer = txtCorrectAnswer.Text;
                question.WrongAnswerOne = txtWrongAnswer1.Text;
                question.WrongAnswerTwo = txtWrongAnswer2.Text;
                question.WrongAnswerThree = txtWrongAnswer3.Text;
                tempquestions.Add(question);
                RefreshFields();
            }
        }
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
        public void RefreshFields()
        {
            txtQuestion.Text = string.Empty;
            txtCorrectAnswer.Text = string.Empty;
            txtWrongAnswer1.Text = string.Empty;
            txtWrongAnswer2.Text = string.Empty;
            txtWrongAnswer3.Text = string.Empty;
            txtFileName.Text = "Filename";
            txtFileName.Foreground = Brushes.LightSlateGray;
            txtFileName.GotFocus += txtFileName_GotFocus;
            lbQuestions.ItemsSource = null;
            lbQuestions.ItemsSource = tempquestions;
        }

        private void txtFileName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.Foreground = Brushes.Black;
            tb.GotFocus -= txtFileName_GotFocus;
        }
    }
}
