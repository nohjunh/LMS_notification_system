using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class LmsData3 :INotifyPropertyChanged
    {
        private string lmsSubject3;
        private string lmsTitle3;
        private string lmsRdate3;

        public string LmsSubject3
        {
            get
            {
                return lmsSubject3;
            }
            set
            {
                lmsSubject3 = value;
                OnPropertyChanged("LmsSubject3");
            }
        }

        public string LmsTitle3
        {
            get
            {
                return lmsTitle3;
            }
            set
            {
                lmsTitle3 = value;
                OnPropertyChanged("LmsTitle3");
            }
        }
        public string LmsRdate3
        {
            get
            {
                return lmsRdate3;
            }
            set
            {
                lmsRdate3 = value;
                OnPropertyChanged("LmsRdate3");
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
