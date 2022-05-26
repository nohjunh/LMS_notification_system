using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class ExcelData2
    {
        private string eLmsSubject2;
        private string eLmsTitle2;
        private string eLmsRdate2;
        private string eLmsEnddate2;

        public string ELmsSubject2
        {
            get{ return eLmsSubject2;}
            set{eLmsSubject2 = value; OnPropertyChanged("ELmsSubject2");}
        }
        public string ELmsTitle2
        {
            get { return eLmsTitle2; }
            set { eLmsTitle2 = value; OnPropertyChanged("ELmsTitle2"); }
        }
        public string ELmsRdate2
        {
            get { return eLmsRdate2; }
            set { eLmsRdate2 = value; OnPropertyChanged("ELmsRdatet2"); }
        }
        public string ELmsEnddate2
        {
            get { return eLmsEnddate2; }
            set { eLmsEnddate2 = value; OnPropertyChanged("ELmsEnddate2"); }
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
