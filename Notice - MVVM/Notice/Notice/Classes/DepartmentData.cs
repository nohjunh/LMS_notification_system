using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Classes
{
    public class DepartmentData : INotifyPropertyChanged
    {
        private string d_num;
        private string d_title;
        private string d_writer;
        private string d_rdate;
        public string D_Num
        {
            get
            {
                return d_num;
            }
            set
            {
                d_num = value;
                OnPropertyUpdate("D_Num");
            }
        }
        public string D_Title
        {
            get
            {
                return d_title;
            }
            set
            {
                d_title = value;
                OnPropertyUpdate("D_Title");
            }
        }
        public string D_Writer
        {
            get
            {
                return d_writer;
            }
            set
            {
                d_writer = value;
                OnPropertyUpdate("D_Writer");
            }
        }
        public string D_Rdate
        {
            get
            {
                return d_rdate;
            }
            set
            {
                d_rdate = value;
                OnPropertyUpdate("D_Rdate");
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
