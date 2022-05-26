using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notice.Model
{
    public class ExcelData
    {
        public string ELmsSubject { get; set; }
        public string ELmsTitle { get; set; }
        public string ELmsRdata { get; set; }

        //위의 데이터를 저장할 객체
        private static List<ExcelData> E_Data;
        public static List<ExcelData> GetE_Data()
        {
            if (E_Data == null)
                E_Data = new List<ExcelData>();
            return E_Data;
        }
    }
}
