using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
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
