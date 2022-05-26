using crawling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using crawling.ViewModel;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace crawling.ViewModel.Command
{
    public class CompareCmd : ICommand
    {
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;

        public CrawlingVM VM { get; set; }
        public DataLmsCmd dataLmscmd { get; set; }

        //ICommand
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if( dataLmscmd.CountBtn2()== 0)
            { MessageBox.Show("비교할 데이터가 없습니다."); }
            else
            {
                readExcel();
                compareData();
            }
        }


        public void readExcel()
        {
            try
            {
                excelApp = new Excel.Application();

                //엑셀 파일 열기
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string path = System.IO.Path.Combine(desktopPath, "강의자료.xlsx");
                workBook = excelApp.Workbooks.Open(path);
                //첫 번째 Worksheet
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;
                //현재 Worksheet에서 일부 범위만 선택 → 속도를 위해
                Excel.Range rng = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[7, 7]];
                //Range 데이타를 배열 (One-based array)로
                object[,] data = rng.Value;


                //excelData에 기록.
                for (int r = 2; r <= data.GetLength(1); r++)
                {
                    ExcelData.GetE_Data().Add(new ExcelData() { ELmsSubject = data[r, 1].ToString(), ELmsTitle = data[r, 2].ToString(), ELmsRdata = data[r, 3].ToString() });
                }

                workBook.Close(true);
                excelApp.Quit();
            }
            finally
            {
                ReleaseObject(workSheet);
                ReleaseObject(workBook);
                ReleaseObject(excelApp);
            }
        }
        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally { GC.Collect(); }
        }


        //readExcel()한 것과 Crawling 한 것 비교
        public void compareData()
        {
            for (int i = 0; i < VM.getCount1(); i++)
            {
                var lmsData1_Title = VM.getList1().ElementAt(i).LmsTitle;
                var excelData_Title = ExcelData.GetE_Data().ElementAt(i).ELmsTitle;

                if (lmsData1_Title != excelData_Title)
                {
                    MessageBox.Show(VM.getList1().ElementAt(i).LmsSubject + "의 내용이 다릅니다.(업로드 되었습니다.)");
                }
                else
                {
                    MessageBox.Show(VM.getList1().ElementAt(i).LmsSubject + "의 내용이 같습니다.(업로드 되지 않았습니다.)");
                }
            }
        }
    }
}
