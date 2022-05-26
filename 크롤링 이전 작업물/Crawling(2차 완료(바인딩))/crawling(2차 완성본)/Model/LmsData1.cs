using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crawling.Model
{
    public class LmsData1 : INotifyPropertyChanged
    {
        private string lmsSubject;
        private string lmsTitle;
        private string lmsWriter;
        private string lmsRdate;

        public string LmsSubject
        {
            get
            {
                return lmsSubject;
            }
            set
            {
                lmsSubject = value;
                OnPropertyChanged("LmsSubject");
            }
        }

        public string LmsTitle
        {
            get
            {
                return lmsTitle;
            }
            set
            {
                lmsTitle = value;
                OnPropertyChanged("LmsTitle");
            }
        }
        public string LmsWriter
        {
            get
            {
                return lmsWriter;
            }
            set
            {
                lmsWriter = value;
                OnPropertyChanged("LmsWriter");
            }
        }
        public string LmsRdate
        {
            get
            {
                return lmsRdate;
            }
            set
            {
                lmsRdate = value;
                OnPropertyChanged("LmsRdate");
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
