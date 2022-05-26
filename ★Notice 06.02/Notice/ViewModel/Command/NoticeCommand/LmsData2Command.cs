using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
//Selenium 
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using System.Windows.Input;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows;
using Notice.Model;
using System.Diagnostics;
using System.Timers;
using System.Windows.Threading;
using static Notice.ViewModel.ViewModel;

namespace Notice.ViewModel.Command.NoticeCommand
{
	/// <summary>
	/// 레포트
	/// </summary>
    public class LmsData2Command : ICommand
	{
		int countBtn3 = 0; // 처음 실행이 아님을 확인
		int countExcel = 0;

		private Timer timer;

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		static Excel.Application excelApp = null;
		static Excel.Workbook workBook = null;
		static Excel.Worksheet workSheet = null;

		public ViewModel VM { get; set; }
		public LmsData2Command(ViewModel vm)
		{
			VM = vm;
		}


		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			//누르자마자 처음은 바로 시작하게 바꾸기
			timer = new Timer();
			timer.Interval = 1000 * 60;//한 시간
			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Enabled = true;
			timer.Start();

			VM.L_Data2.Add(new LmsData2()
			{
				LmsTitle2 = "데이터 로딩중"
			});
			VM.get2();
			VM.L_Data2.Clear();
			Start3();
			countBtn3++;
		}

		private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

			DispatcherService.Invoke((System.Action)(() =>{
				if (countBtn3 != 0)
				{
					VM.L_Data2_Main.Clear();
					VM.L_Data2.Clear();
				}
				VM.L_Data2.Add(new LmsData2()
				{
					LmsTitle2 = "데이터 로딩중"
				});
				VM.get2();
				VM.L_Data2.Clear();
				Start3();
				countBtn3++;
			}));
        }

        private async void Start3()
		{
			var task3 = Task.Run(() => ReportCrawling());
			await task3;
			VM.get2();

			if (countExcel==0) { saveExcel(); countExcel++; }
			else
            {
				//VM.E_Data2.Clear();
				readExcel();
				compareData();
				saveExcel();
            }

		}

		public void ReportCrawling()
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
				if (element != null)
                {
					element.Click();
                }
			}
			catch (Exception)
			{
				MessageBox.Show("ID,PW를 확인해주세요.");
				return;
			}

			element = _driver.FindElementByXPath("//*[@id='nav']/li[5]/a");
			element.Click();

            try
            {
				for (int i = 2; i <= _driver.FindElements(By.XPath("//*[@id='treeboxtab']/div/table/tbody/tr")).Count; i++)
				{

					element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
					element.Click();
					string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					TextUpLoad3();
				}
			}catch(Exception)
            {
				return;
            }
			_driver.Close();
		}

		public void TextUpLoad3()
		{
			if (_driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]").Text == "해당하는 레포트 정보가 없습니다.")
			{
				VM.L_Data2.Add(new LmsData2()
				{
					LmsSubject2 = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle2 = "업로드된 레포트가 없습니다.",
					LmsRdate2 = "-",
					LmsEndDate2 = "-"
				}) ;
				return;
			}
			else
			{
				VM.L_Data2.Add(new LmsData2()
				{
					LmsSubject2 = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsEndDate2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[3]").Text,
					LmsTitle2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[2]").Text,
					LmsRdate2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[6]").Text
				});
			}
		}

		public void saveExcel()
		{
			try
			{
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string path2 = System.IO.Path.Combine(desktopPath, "레포트.xlsx");

				excelApp = new Excel.Application();
				excelApp.DisplayAlerts = false;
				workBook = excelApp.Workbooks.Add();
				workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

				workSheet.Cells[1, 1] = "과 목";
				workSheet.Cells[1, 2] = "제 목";
				workSheet.Cells[1, 3] = "제출일시";
				workSheet.Cells[1, 4] = "작성일";

				for (int i = 0; i < VM.getCount2(); i++)
				{
					workSheet.Cells[2 + i, 1] = VM.getList2().ElementAt(i).LmsSubject2;
					workSheet.Cells[2 + i, 2] = VM.getList2().ElementAt(i).LmsTitle2;
					workSheet.Cells[2 + i, 3] = VM.getList2().ElementAt(i).LmsEndDate2;
					workSheet.Cells[2 + i, 4] = VM.getList2().ElementAt(i).LmsRdate2;
				}
				workSheet.Columns.AutoFit();
				workSheet.SaveAs(path2, Excel.XlFileFormat.xlWorkbookDefault);
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
				string path = System.IO.Path.Combine(desktopPath, "레포트.xlsx");
				workBook = excelApp.Workbooks.Open(path);
				workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

				Excel.Range rng = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[9, 9]];
				object[,] data = rng.Value;

				//excelData에 기록.
				for (int r = 2; r <= data.GetLength(1); r++)
				{
					if (data[r, 1] != null && data[r, 2] != null && data[r, 3] != null && data[r,4]!=null)
                    {
						VM.E_Data2.Add(new ExcelData2() {  ELmsSubject2= data[r, 1].ToString(), ELmsTitle2 = data[r, 2].ToString(), ELmsEnddate2 = data[r, 3].ToString(), ELmsRdate2=data[r,4].ToString() });
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
			//오류 있음
			for (int i = 0; i < VM.getCount2(); i++)
			{
				var lmsData1_Title = VM.getList2().ElementAt(i).LmsTitle2;
				var excelData_Title = VM.E_Data2.ElementAt(i).ELmsTitle2;

				if (lmsData1_Title != excelData_Title)
				{
					if (NaverData.userEmail != null)
					{
						VM.naverMailManager.sendMail(VM.getList2().ElementAt(i).LmsSubject2 + "의 내용이 다릅니다.(마지막 비교와 비교해서 새 과제(레포트)가 업로드 되었습니다.)" + "</h2><h3><a href=" + "https://ieilms.jbnu.ac.kr" + ">LMS에서 바로 확인하기</a></h3>");
                    }
                    else
                    {
						MessageBox.Show(VM.getList2().ElementAt(i).LmsSubject2 + "의 내용이 다릅니다.(마지막 비교와 비교해서 새 과제(레포트)가 업로드 되었습니다.)");
                    }
				}
			}
		}
	}
}