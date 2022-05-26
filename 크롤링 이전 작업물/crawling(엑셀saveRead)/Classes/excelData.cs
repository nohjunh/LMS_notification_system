using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crawling.Classes
{
    class excelData
    {
        public string ELmsSubject { get; set; }
        public string ELmsTitle { get; set; }
        public string ELmsRdata { get; set; }

        //위의 데이터를 저장할 객체
        private static List<excelData> E_Data;
        public static List<excelData> GetE_Data()
        {
            if (E_Data == null)
                E_Data = new List<excelData>();
            return E_Data;
        }
    }
}
