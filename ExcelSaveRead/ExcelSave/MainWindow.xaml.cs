using ExcelSave.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;


namespace ExcelSave
{
    public partial class MainWindow : Window
    {
        //필드
        static Excel.Application excelApp = null;
        static Excel.Workbook workBook = null;
        static Excel.Worksheet workSheet = null;

        public MainWindow()
        {
            InitializeComponent();
            //데이터 추가
            beverageData.GetInstance().Add(new beverageData() { kind = "탄산", name = "환타", price = 1100 });
            beverageData.GetInstance().Add(new beverageData() { kind = "탄산", name = "콜라", price = 1000 });
            beverageData.GetInstance().Add(new beverageData() { kind = "비탄산", name = "토레타", price = 1800 });
            //xaml부분에서 추가
            beverageView.ItemsSource = beverageData.GetInstance();

            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string path = System.IO.Path.Combine(desktopPath, "Excel.xlsx");

                excelApp = new Excel.Application();
                workBook = excelApp.Workbooks.Add();
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

                workSheet.Cells[1, 1] = c1.Header.ToString();
                workSheet.Cells[1, 2] = c2.Header.ToString();
                workSheet.Cells[1, 3] = c3.Header.ToString();

                for(int i = 0; i<beverageView.Items.Count;i++)
                {
                    beverageData b_Data = beverageData.GetInstance().ElementAt(i);
                    workSheet.Cells[2 + i, 1] = b_Data.kind;
                    workSheet.Cells[2 + i, 2] = b_Data.name;
                    workSheet.Cells[2 + i, 3] = b_Data.price;
                }

                workSheet.Columns.AutoFit();
                workSheet.SaveAs(path, Excel.XlFileFormat.xlWorkbookDefault);
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
                if(obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch(Exception ex) 
            { 
                obj = null; 
                throw ex; 
            }
            finally { GC.Collect(); }
        }

        private void readButton_Click(object sender, RoutedEventArgs e)
        {
            ReadExcel();
            MessageBox.Show("read 끝!");
        }

        public void ReadExcel()
        {
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();

                //엑셀 파일 열기
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string path = System.IO.Path.Combine(desktopPath, "Excel.xlsx");
                wb = excelApp.Workbooks.Open(path);
                //첫 번째 Worksheet
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                //현재 Worksheet에서 일부 범위만 선택 → 속도를 위해
                Excel.Range rng = ws.Range[ws.Cells[1, 1], ws.Cells[4, 9]];
                //Range 데이타를 배열 (One-based array)로
                object[,] data = rng.Value;

                //excelData에 기록.
                for (int r = 2; r <= data.GetLength(0); r++)
                {
                    excelData.GetE_Data().Add(new excelData() { Ekind = data[r, 1].ToString(), Ename = data[r, 2].ToString(), Eprice = Int32.Parse(data[r, 3].ToString()) });
                }

                wb.Close(true);
                excelApp.Quit();
            }
            finally
            {
                ReleaseObject(ws);
                ReleaseObject(wb);
                ReleaseObject(excelApp);
            }
        }
        //비교하기 
        private void compareButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> CompareList = new List<string>();
            
            for (int i = 0; i<=beverageData.GetInstance().Count;i++)
            {
                var bData_Name = beverageData.GetInstance().ElementAt(i).name;
                var eDate_EName = excelData.GetE_Data().ElementAt(i).Ename;

                if(bData_Name!= eDate_EName)
                {
                    CompareList.Add(bData_Name);
                    MessageBox.Show(CompareList.ElementAt(i) + "의 내용이 다릅니다!");
                }
                else
                {
                    CompareList.Add(bData_Name);
                    MessageBox.Show(CompareList.ElementAt(i) + "이 같음! Test message");
                }
            }

        }
    }
}
