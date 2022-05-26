using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class MainWindowIDInformation : INotifyPropertyChanged
    {
        //ID정보 저장
        private string _loginId;
        private string _loginPasswd;

        public string LoginID
        {
            get
            {
                return _loginId;
            }
            set
            {
                _loginId = value;
                OnPropertyUpdate("LoginID");
            }
        }
        public string LoginPasswd
        {
            get
            {
                return _loginPasswd;
            }
            set
            {
                _loginPasswd = value;
                OnPropertyUpdate("LoginPasswd");
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

