using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notice.Model;
using System.Windows;
//Selenium 
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
using static Notice.ViewModel.ViewModel;

namespace Notice.ViewModel.Command
{
	/// <summary>
	/// 강의 자료
	/// </summary>
    public class LmsData1Command : ICommand
    {
		private Timer timer;
		int countBtn2 = 0;
		bool countExcel = false;

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		static Excel.Application excelApp = null;
		static Excel.Workbook workBook = null;
		static Excel.Worksheet workSheet = null;
		
		public ViewModel VM { get; set; }
		public LmsData1Command(ViewModel vm)
		{
			VM = vm;
		}

		//ICommand 인터페이스 구현
		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			//누르자마자 처음은 바로 시작하게 바꾸기,
			timer = new Timer();
			timer.Interval = 1000 * 60; // 한 시간 간격
            timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Enabled = true;
			timer.Start();

			VM.L_Data1.Add(new LmsData1()
			{
				LmsTitle = "데이터 로딩중"
			});
			VM.get1();
			VM.L_Data1.Clear();
			Start2();
			countBtn2++;
		}

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
			DispatcherService.Invoke((System.Action)(() =>
			{
				if (countBtn2 != 0)
				{
					VM.L_Data1_Main.Clear();
					VM.L_Data1.Clear();
				}
				VM.L_Data1.Add(new LmsData1()
				{
					LmsTitle = "데이터 로딩중"
				});
				VM.get1();
				VM.L_Data1.Clear();
				Start2();
				countBtn2++;
			}));
		}

		//메소드
		private async void Start2()
		{
			var task2 = Task.Run(() => DataCrawling());
			await task2;
			VM.get1();

			if (!countExcel) { saveExcel(); countExcel = true; }
			else
            {
				readExcel();
				compareData();
				//기존에 ~파일이 있습니다 메시지가 한번 갱신 될때마다 떠서 일일이 눌러줘야 하는 불편함이 있음, 변경된 내용만 바꾸던가 메시지 창이 안뜨고 자동으로 바꾸든가
				saveExcel();
            }
		}

		public void DataCrawling()
		{
			_driverService = ChromeDriverService.CreateDefaultService();
			_driverService.HideCommandPromptWindow = true;
			_options = new ChromeOptions();
			_options.AddArgument("headless");
			_options.AddArgument("disable-gpu");
			_driver = new ChromeDriver(_driverService, _options);


			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement element;
			try
			{
				element = _driver.FindElementByXPath("//*[@id='id']");
				element.SendKeys(VM.mainWindowIDInformation.LoginID);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(VM.mainWindowIDInformation.LoginPasswd);

				element = _driver.FindElementByXPath("//*[@id='loginform']/table/tbody/tr[1]/td[2]/input");
				element.Click();

				element = _driver.FindElementByXPath("//*[@id='boardAbox']/form/table/tbody/tr[1]/td[2]");
				element.Click();
			}
			catch (Exception)
			{
				MessageBox.Show("ID,PW를 확인해주세요.");
				return;
			}

			element = _driver.FindElementByXPath("//*[@id='nav']/li[4]/a");
			element.Click();

	
				for (int i = 2; i <= _driver.FindElements(By.XPath("//*[@id='treeboxtab']/div/table/tbody/tr[2]/td[2]/table/tbody/tr")).Count; i++)
				{
					element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
					element.Click();
					string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[2]/td[2]/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					TextUpLoad2();
				}
			_driver.Close();
		}

		public void TextUpLoad2()
		{
			if (_driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr").Text == "해당하는 자료 정보가 없습니다.")
			{
				VM.L_Data1.Add(new LmsData1()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = "업로드된 자료가 없습니다.",
					LmsRdate = "-"
				});
				return;
			}
			else
			{
				VM.L_Data1.Add(new LmsData1()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = _driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr[1]/td[2]").Text,
					LmsRdate = _driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr[1]/td[4]").Text,

				});
			}
		}

		public void saveExcel()
        {
			try
			{
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string path1 = System.IO.Path.Combine(desktopPath, "강의자료.xlsx");

				excelApp = new Excel.Application();
				excelApp.DisplayAlerts = false;
				workBook = excelApp.Workbooks.Add();
				workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

				workSheet.Cells[1, 1] = "강의명";
				workSheet.Cells[1, 2] = "제 목";
				workSheet.Cells[1, 3] = "작성일";

				for (int i = 0; i < VM.getCount1(); i++)
				{
					workSheet.Cells[2 + i, 1] = VM.getList1().ElementAt(i).LmsSubject;
					workSheet.Cells[2 + i, 2] = VM.getList1().ElementAt(i).LmsTitle;
					workSheet.Cells[2 + i, 3] = VM.getList1().ElementAt(i).LmsRdate;
				}
				workSheet.Columns.AutoFit();
				workSheet.SaveAs(path1, Excel.XlFileFormat.xlWorkbookDefault);
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
		public void readExcel()
		{
			try
			{
				excelApp = new Excel.Application();

				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string path = System.IO.Path.Combine(desktopPath, "강의자료.xlsx");
				workBook = excelApp.Workbooks.Open(path);
				workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

				Excel.Range rng = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[9, 9]];
				object[,] data = rng.Value;

				//excelData에 기록.
				for (int r = 2; r <= data.GetLength(1); r++)
				{
					if (data[r, 1]!= null && data[r, 2] != null && data[r, 3] != null)
                    {
						VM.E_Data.Add(new ExcelData() { ELmsSubject = data[r, 1].ToString(), ELmsTitle = data[r, 2].ToString(), ELmsRdata = data[r, 3].ToString() });
                    }
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
		public void compareData()
		{
			for (int i = 0; i < VM.getCount1(); i++)
			{
				var lmsData1_Title = VM.getList1().ElementAt(i).LmsTitle;
				var excelData_Title = VM.E_Data.ElementAt(i).ELmsTitle;

				if (lmsData1_Title != excelData_Title)
				{
                    if (NaverData.userEmail != null)
                    {
						VM.naverMailManager.sendMail(VM.getList1().ElementAt(i).LmsSubject + "의 강의자료 내용이 다릅니다.(업로드 되었습니다.)"+"</h2><h3><a href="+"https://ieilms.jbnu.ac.kr"+">LMS에서 바로 확인하기</a></h3>");

					}
                    else
                    {
						MessageBox.Show(VM.getList1().ElementAt(i).LmsSubject + "의 내용이 강의자료 내용이 다릅니다.(업로드 되었습니다.)");
                    }
					
				}
			}
		}
	}
}
