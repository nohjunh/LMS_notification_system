using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class ExcelData : INotifyPropertyChanged
    {
        private string eLmsSubject { get; set; }
        private string eLmsTitle { get; set; }
        private string eLmsRdata { get; set; }

        public string ELmsSubject
        {
            get { return eLmsSubject; }
            set { eLmsSubject = value; OnPropertyChagned("ELmsSubject"); }
        }
        public string ELmsTitle
        {
            get { return eLmsTitle; }
            set { eLmsTitle = value; OnPropertyChagned("ELmsTitle"); }
        }
        public string ELmsRdata
        {
            get { return eLmsRdata; }
            set { eLmsRdata = value; OnPropertyChagned("ELmsRdata"); }
        }

        ////위의 데이터를 저장할 객체
        //private static List<ExcelData> E_Data;
        //public static List<ExcelData> GetE_Data()
        //{
        //    if (E_Data == null)
        //        E_Data = new List<ExcelData>();
        //    return E_Data;
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChagned(string propertyName)
        {
            if(PropertyChanged !=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
