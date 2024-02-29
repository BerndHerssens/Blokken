using System;
using System.ComponentModel;

namespace Groepsproject_Blokken
{
    public partial class Manager : INotifyPropertyChanged, IDataErrorInfo
    {
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "Name")
                {
                    if (String.IsNullOrEmpty(Name))
                    {
                        result = "Geef een naam in";
                    }
                }
                return result;
            }
        }

        public string NameValidation
        {
            get { return Name; }
            set
            {
                Name = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
