using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
//Todo nog niks :)
namespace Groepsproject_Blokken
{
    public partial class Player : INotifyPropertyChanged, IDataErrorInfo
    {
        private string _nameValidation;
        private string _passwordValidation;
        private string _passwordConfirmValidation;

        public event PropertyChangedEventHandler PropertyChanged;
        [System.Text.Json.Serialization.JsonIgnore]

        public string NameValidation
        {
            get { return _nameValidation; }
            set
            {
                OnPropertyChanged(ref _nameValidation, value);
            }
        }
        [System.Text.Json.Serialization.JsonIgnore]

        public string PasswordValidation
        {
            get { return _passwordValidation; }
            set
            {
                OnPropertyChanged(ref _passwordValidation, value);
            }
        }
        [System.Text.Json.Serialization.JsonIgnore]

        public string PasswordConfirmValidation
        {
            get { return _passwordConfirmValidation; }
            set
            {
                OnPropertyChanged(ref _passwordConfirmValidation, value);
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "NameValidation")
                {
                    if (string.IsNullOrEmpty(NameValidation))
                    {
                        result = "Geef een naam in.";
                    }
                }
                if (columnName == "PasswordValidation")
                {
                    if (string.IsNullOrEmpty(PasswordValidation))
                    {
                        result = "Geef een paswoord in.";
                    }
                }
                if (columnName == "PasswordConfirmValidation")
                {
                    if (string.IsNullOrEmpty(PasswordConfirmValidation))
                    {
                        result = "Gelieve uw paswoord te bevestigen.";
                    }
                }
                return result;
            }
        }
        [System.Text.Json.Serialization.JsonIgnore]
        public string Error => throw new System.Exception();

        [System.Text.Json.Serialization.JsonIgnore]
        public int Position { get; internal set; }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value, [CallerMemberName] string propertyName = "")
        {
            bool isChanged = true;
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                isChanged = false;
            }

            backingField = value;
            OnPropertyChanged(propertyName);
            return isChanged;
        }
        //Als je beide values tegelijk wilt met text erbij
        public string CalculateWinRates() //Geeft beide winrates
        {
            var WinrateSPVS = "Winrate in SP: " + SPGamesWon / SPGamesPlayed * 100 + " %" + Environment.NewLine + "Winrate in VS: " + VSGamesWon / VSGamesPlayed + " %";
            if (this.SPGamesPlayed == null || this.VSGamesPlayed == null)  //Als een van deze null zijn dan geven we N/A mee met deze functie
            {
                WinrateSPVS = "N/A";
            }

            return WinrateSPVS; //Gooit regel 10 indien het niet null is, regel 13 als een van de twee null is.
        }
        // Als je het in een txtvak wilt gooien en je het wilt opsplitsen :)
        public string CalculateSPWinRate() //Zelfde logica dan bij functie CalculateWinRates();
        {
            var WinrateSP = Convert.ToString(SPGamesWon / SPGamesPlayed * 100) + "%";
            if (this.SPGamesPlayed == null)
            {
                WinrateSP = "No games played yet!";
            }
            return WinrateSP;
        }
        public string CalculateVSWinRate()
        {
            var WinrateVS = Convert.ToString(VSGamesWon / VSGamesPlayed * 100) + "%";
            if (this.VSGamesPlayed == null)
            {
                WinrateVS = "No games played yet!";
            }
            return WinrateVS;
        }
        public override bool Equals(object obj)
        {
            bool resultaat = false;
            if (obj != null)
            {
                if (GetType() == obj.GetType())
                {
                    Player g = (Player)obj;
                    if (this.Name == g.Name)
                    {
                        resultaat = true;
                    }
                }
            }
            return resultaat;
        }
        public override string ToString()
        {
            return this.Name;
        }

    }
}

