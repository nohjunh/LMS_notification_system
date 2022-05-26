using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ExcelSave.Classes
{
    class excelData
    {
        //Binding 될 data들은 get;set;을 꼭!
        public string Ekind { get; set; }
        public string Ename { get; set; }
        public int Eprice { get; set; }


        //저장하는 객체(사용 : 바인딩될 데이터를 저장할 변수만)
        private static List<excelData> E_Data;
        //인스턴스 얻어오기
        public static List<excelData> GetE_Data()
        {
            if (E_Data == null)
                E_Data = new List<excelData>();
            return E_Data;
        }


    }
}

