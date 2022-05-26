using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class ExcelData3
    {
        private string eLmsSubject3;
        private string eLmsTitle3;
        private string eLmsRdate3;

        public string ELmsSubject3
        {
            get{ return eLmsSubject3;}
            set{eLmsSubject3 = value; OnPropertyChanged("ELmsSubject3");}
        }
        public string ELmsTitle3
        {
            get { return eLmsTitle3; }
            set { eLmsTitle3 = value; OnPropertyChanged("ELmsTitle3"); }
        }
        public string ELmsRdate3
        {
            get { return eLmsRdate3; }
            set { eLmsRdate3 = value; OnPropertyChanged("ELmsRdatet3"); }
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
