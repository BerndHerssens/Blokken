﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
//TODO: (nog) niks
namespace Groepsproject_Blokken
{
    internal class Question
    {
        //Attributen
        private string _question;
        private string _correctAnswer;
        private string _wrongAnswerOne;
        private string _wrongAnswerTwo;
        private string _wrongAnswerThree;
        private int _questionDisplayTime;
        private DispatcherTimer _timer;

        //Constr
        public Question(string aQuestion, string aCorrectAnswer, string aWrongAnswerOne, string aWrongAnswerTwo, string aWrongAnswerThree)
        {
            TheQuestion = aQuestion;
            CorrectAnswer = aCorrectAnswer;
            WrongAnswerOne = aWrongAnswerOne;
            WrongAnswerTwo = aWrongAnswerTwo;
            WrongAnswerThree = aWrongAnswerThree;
        }
        //Properties in volgorde , met het juiste antwoord als eerste
        public string TheQuestion
        {
            get { return _question; }
            set { _question = value; }
        }
        public string CorrectAnswer
        {
            get { return _correctAnswer; }
            set { _correctAnswer = value; }
        }
        public string WrongAnswerOne
        {
            get { return _wrongAnswerOne; }
            set { _wrongAnswerOne = value; }
        }
        public string WrongAnswerTwo
        {
            get { return _wrongAnswerTwo; }
            set { _wrongAnswerTwo = value; }
        }
        public string WrongAnswerThree
        {
            get { return _wrongAnswerThree; }
            set { _wrongAnswerThree = value; }
        }
        public int QuestionDisplayTime
        {
            get { return _questionDisplayTime; }
            set { _questionDisplayTime = value; }
        }
        public DispatcherTimer Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }

        //Methodes Timer
        public void StartTimer() //Settings van timer instellen dan -> start de timer - Dit kon mss in constructor (?)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += Timer_Tick;
            Timer.Start();

        }
        public void Timer_Tick(object sender, EventArgs e) //Elke tick (1 tick = 1 seconde) , de displaytime met 1 naar beneden, als de displaytime 0 is, stop de timer
        {
            QuestionDisplayTime--;
            if (QuestionDisplayTime == 0)
            {
                Timer.Stop();
            }
        }

        //Methodes Answers/Buttons
        //Antwoorden in random buttons steken
        public void InsertAnswersInButtons(Button topLeftButton1, Button topRightButton2, Button bottomLeftButton3, Button bottomRightButton4)
        {
            Random random = new Random();
            switch (random.Next(0, 5)) //Rollt values 0,1,2,3 en 4
            {
                case 0:
                    topLeftButton1.Content = CorrectAnswer;
                    topRightButton2.Content = WrongAnswerOne;
                    bottomLeftButton3.Content = WrongAnswerTwo;
                    bottomRightButton4.Content = WrongAnswerThree;
                    break;
                case 1:
                    topLeftButton1.Content = WrongAnswerTwo;
                    topRightButton2.Content = CorrectAnswer;
                    bottomLeftButton3.Content = WrongAnswerThree;
                    bottomRightButton4.Content = WrongAnswerOne;
                    break;
                case 2:
                    topLeftButton1.Content = WrongAnswerOne;
                    topRightButton2.Content = WrongAnswerThree;
                    bottomLeftButton3.Content = CorrectAnswer;
                    bottomRightButton4.Content = WrongAnswerTwo;
                    break;
                case 3:
                    topLeftButton1.Content = WrongAnswerThree;
                    topRightButton2.Content = WrongAnswerTwo;
                    bottomLeftButton3.Content = WrongAnswerOne;
                    bottomRightButton4.Content = CorrectAnswer;
                    break;
                case 4:
                    topLeftButton1.Content = CorrectAnswer; ;
                    topRightButton2.Content = WrongAnswerTwo;
                    bottomLeftButton3.Content = WrongAnswerOne;
                    bottomRightButton4.Content = WrongAnswerThree;
                    break;
            }

        }
        //Highlight if wrong or correct answer
        public void AnswerHighlight(Button selectedButton)
        {
            if ((string)selectedButton.Content == CorrectAnswer)
            {
                selectedButton.Background = Brushes.Green;
            }
            else
            {
                selectedButton.Background = Brushes.OrangeRed;
            }
        }
        //Looped door de buttons en laat het correcte antwoord zien (gebruiken wanneer niemand het juist heeft)
        public void ShowCorrectAnswer(List<Button> lijstButtons)
        {
            foreach (Button button in lijstButtons)
            {
                if ((string)button.Content == CorrectAnswer)
                {
                    button.Background = Brushes.Green;
                }
            }
        }








    }
}
