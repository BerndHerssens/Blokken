<<<<<<< HEAD
﻿namespace Groepsproject_Blokken
=======
﻿using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Windows;

namespace Groepsproject_Blokken
>>>>>>> 71729ae5ac7a0a85f96159bcadc0bb1fe542e0bd
{
    internal class PrimeWord
    {
        string _primeword;
        string _hint;
        public PrimeWord()
        {

        }
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
        //TODO: deze zal in de main window moeten
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
<<<<<<< HEAD
        //public static void ReadAndFillPrimeWordList(List<PrimeWord> primeWordList) //i want to make a method that will fill in a list array of primewords, read from a 
        //{
        //    try
        //    {
        //        var filepath = "../../PrimeWords/List.txt";
        //        try
        //        {

        //            StreamReader readobject = new StreamReader(filepath);
        //            string[] temperaryLines;
        //            string[,] partsArray;
        //            var teller = 0;
        //            while (!readobject.EndOfStream)
        //            {
        //                teller++;
        //                readobject.ReadLine();
        //            }
        //            temperaryLines = new string[teller];
        //            for (int i = 0; i < teller; i++)
        //            {
        //                temperaryLines[i] = readobject.ReadLine();
        //            }
        //            partsArray = new string[teller, 2];

        //            for (int i = 0; i < teller; i++)
        //            {
        //                string[] parts = temperaryLines[i].Split(';');


        //                if (parts.Length >= 2)
        //                {
        //                    partsArray[i, 0] = parts[0];  // Left part/Primeword = this.Prime
        //                    this.Primeword = parts[0];
        //                    partsArray[i, 1] = parts[1]; // Right part
        //                    this.Hint = parts[1];
        //                }
        //                else
        //                {

        //                }
        //            }
        //            return primeWordList;
        //        }
        //        catch (FileNotFoundException)
        //        {
        //            MessageBox.Show("File not found. Please check the file path", "Error");
        //        }
        //    }
        //    catch { }
=======
        //TODO: Deze zal in de main window moeten
        public void ReadAndFillPrimeWordList() //i want to make a method that will fill in a 2d array of (prime)words, read from a txt
        {
            try
            {
                var filepath = "../../PrimeWords/List1.txt";
                try
                {

                    StreamReader readobject = new StreamReader(filepath);
                    string[] temperaryLines;
                    string[,] wordsThatAreParted; //TODO: deze moet je in de mainwindow alleen zetten
                    var teller = 0;
                    
                    while (!readobject.EndOfStream)//here i just need the lenght of the list, so i need to increase "teller"
                    {
                        teller++;
                        readobject.ReadLine();
                    }
                    temperaryLines = new string[teller]; //now that i have teller, i can make my length
                    for (int i = 0; i < teller; i++) 
                    {
                        temperaryLines[i] = readobject.ReadLine();
                    }
                    wordsThatAreParted = new string[teller, 2];
                    for (int i = 0; i < teller; i++)
                    {
                        string[] parts = temperaryLines[i].Split(';');
                        if (parts.Length >= 2)
                        {
                            wordsThatAreParted[i, 0] = parts[0];  // Left part this will be the primeword then
                            wordsThatAreParted[i, 1] = parts[1];  // Right part this will be the hint
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
        public void RandomPrimeWordConstruction()
        {
            string[,] wordsThatAreParted; //TODO: deze moeten weg, we spreken de algemene aan
            wordsThatAreParted = new string[10, 2];
            Random myRandom = new Random();
            PrimeWord myPrimeWord;
            var randomNumberToPickAndRemove = myRandom.Next(0, wordsThatAreParted.Length); 
            myPrimeWord = new PrimeWord((wordsThatAreParted[randomNumberToPickAndRemove, 0]), (wordsThatAreParted[randomNumberToPickAndRemove, 1])); //here we have our random prime word with the random Hint
            string[,] writeAwayString = new string[wordsThatAreParted.Length-1, 2]; //TODO: this one we can move to main, so we can get it for the writing for the new TXT
            for (int i = 0, j = 0; i < wordsThatAreParted.GetLength(0); i++)
            {
                if (i == randomNumberToPickAndRemove)
                    continue;

                for (int k = 0, u = 0; k < wordsThatAreParted.GetLength(1); k++)
                {
                    if (k == randomNumberToPickAndRemove)
                        continue;

                    writeAwayString[j, u] = wordsThatAreParted[i, k];
                    u++;
                }
                j++;
            }
            //TODO: hier moet er nog een writer komen voor naar txt1
            WriteAwayToTxtTwo(myPrimeWord);
        }
        public void WriteAwayToTxtTwo(PrimeWord wegschrijvenWord) //ik heb de Primeword nodig
        {
            var filepath = "../../PrimeWords/List2.txt";
            string writeAway;
           
            using (StreamWriter writer = new StreamWriter(filepath))
            {

            }
        }
>>>>>>> 71729ae5ac7a0a85f96159bcadc0bb1fe542e0bd
    }
}


