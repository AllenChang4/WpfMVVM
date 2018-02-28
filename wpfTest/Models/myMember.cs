using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace WpfTest.Models
{
    public class mymember : IEditableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(v));
        }

        private string _firstName;
        public String firstName {
            get { return _firstName; }
            set {
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }


        private string _lastName;
        public String lastName {
            get { return _lastName; }
            set { _lastName=value;
                OnPropertyChanged("LastName");
            }
        }


        private string _gender;
        public String gender
        {
            get { return _gender; }
            set {_gender = value;
                OnPropertyChanged("gender");
            }
        }

        private string _dateOfBirth;
        public String dateOfBirth {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        private int _weight;
        public int weight {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChanged("Weight");
            }
        }

        private string _height;
        public String height {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged("Height");
            }
        }

        private string _phoneNumber;
        public String phoneNumber {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged("PhoneNumber");
            }
        }

        private mymember backupCopy;
        private bool inEdit;

        public void BeginEdit()
        {
            if (inEdit) return;
            inEdit = true;
            backupCopy = this.MemberwiseClone() as mymember;
        }

        public void CancelEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            this.lastName = backupCopy.lastName;
            this.firstName = backupCopy.firstName;
            this._gender = backupCopy.gender;
            this.dateOfBirth = backupCopy.dateOfBirth;
            this.height = backupCopy.height;
            this.weight = backupCopy.weight;
            this.phoneNumber = backupCopy.phoneNumber;
        }

        public void EndEdit()
        {
            if (!inEdit) return;
            inEdit = false;
            backupCopy = null;
        }
    }
    public class myMemberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value,
            System.Globalization.CultureInfo cultureInfo)
        {
            mymember item = (value as BindingGroup).Items[0] as mymember;
            if (item.weight > 500)
            {
                    return new ValidationResult(false,
                        "weight is too high");

            }
            else if (item.weight > 50)
            {
                return new ValidationResult(false,
                    "weight is too low");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
        }
    }
}
