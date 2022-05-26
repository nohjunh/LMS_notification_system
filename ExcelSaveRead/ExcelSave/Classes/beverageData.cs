using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ExcelSave.Classes
{
    class beverageData
    {
        //Binding 될 data들은 get;set;을 꼭!
        public string kind { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        //리스트뷰에 들어갈 데이터를 저장하는 객체(사용 : 바인딩될 데이터를 저장할 변수만)
        private static List<beverageData> instance;
        //인스턴스 얻어오기
        public static List<beverageData> GetInstance()
        {
            if (instance == null)
                instance = new List<beverageData>();
            return instance;
        }
    }
}
