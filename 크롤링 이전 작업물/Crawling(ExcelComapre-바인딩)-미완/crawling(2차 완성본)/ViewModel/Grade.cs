using crawling.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace crawling.Model
{
    public class Grade : INotifyPropertyChanged
    {   
        public ICommand MyCommand { get; set; }
        private string gradeNumber;
        public string GradeNumber
        {
            get {return gradeNumber;}
            set
            {
                gradeNumber = value;
                OnPropertyUpdate("GradeNumber");
            }
        }

        public Grade()
        {
            MyCommand = new GradeCmd(executemethod, canexecutemethod);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private bool canexecutemethod(object parameter)
        {
            if (parameter != null) { return true; }
            else return false;
        }
        private void executemethod(object parameter)
        {
            GradeNumber = (string)parameter;
        }
    }
}
