using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class LmsData2 : INotifyPropertyChanged
    {
        private string lmsSubject2;
        private string lmsTitle2;
        private string lmsRdate2;
        private string lmsEnddate2;

        public string LmsSubject2
        {
            get
            {
                return lmsSubject2;
            }
            set
            {
                lmsSubject2 = value;
                OnPropertyChanged("LmsSubject2");
            }
        }

        public string LmsTitle2
        {
            get
            {
                return lmsTitle2;
            }
            set
            {
                lmsTitle2 = value;
                OnPropertyChanged("LmsTitle2");
            }
        }
        public string LmsRdate2
        {
            get
            {
                return lmsRdate2;
            }
            set
            {
                lmsRdate2 = value;
                OnPropertyChanged("LmsRdate2");
            }
        }
        public string LmsEndDate2
        {
            get
            {
                return lmsEnddate2;
            }
            set
            {
                lmsEnddate2 = value;
                OnPropertyChanged("LmsEndDate2");
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
