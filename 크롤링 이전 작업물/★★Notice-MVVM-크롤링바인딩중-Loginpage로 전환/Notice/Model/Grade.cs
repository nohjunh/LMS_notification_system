using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class Grade : INotifyPropertyChanged
    {
        private int gradeNumber;
        public int GradeNumber
        {
            get
            {
                return gradeNumber;
            }
            set
            {
                gradeNumber = value;
                OnPropertyUpdate("GradeNumber");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyUpdate(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
