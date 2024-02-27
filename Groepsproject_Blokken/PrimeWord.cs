using System.IO;
using System.Windows;

namespace Groepsproject_Blokken
{
    internal class PrimeWord
    {
        string _primeword;
        string _hint;
        public PrimeWord(string primeword, string hint)
        {
            Primeword = primeword;
            Hint = hint;
        }
        public string Primeword
        {
            get { return _primeword; }
            set { _primeword = value; }
        }
        public string Hint
        {
            get { return _hint; }
            set { _hint = value; }
        }
        public bool CheckAnswerIfPrimeWord(string answer)
        {
            try //added a try catch, in case the answer would be not validated before it gets here
            {
                if (answer == Primeword)
                {
                    return true;
                }
                else
                { return false; }
            }
            catch { return false; }
        }
        public void ReadAndFillPrimeWordList()
        {
            try
            {
                var filepath = "../../PrimeWords/List.txt";
                try
                {

                    StreamReader readobject = new StreamReader(filepath);
                    string[] temperaryLines;
                    string[,] partsArray;
                    var teller = 0;
                    while (!readobject.EndOfStream)
                    {
                        teller++;
                        readobject.ReadLine();
                    }
                    temperaryLines = new string[teller];
                    for (int i = 0; i < teller; i++)
                    {
                        temperaryLines[i] = readobject.ReadLine();
                    }
                    partsArray = new string[teller, 2];

                    for (int i = 0; i < teller; i++)
                    {
                        string[] parts = temperaryLines[i].Split(';');


                        if (parts.Length >= 2)
                        {
                            partsArray[i, 0] = parts[0]; // Left part
                            partsArray[i, 1] = parts[1]; // Right part
                        }
                        else
                        {

                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    MessageBox.Show("File not found. Please check the file path", "Error");
                }
            }
            catch { }
        }
    }
}

