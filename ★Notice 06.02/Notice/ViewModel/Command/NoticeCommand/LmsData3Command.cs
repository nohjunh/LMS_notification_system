using Notice.Model;
//Selenium 
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using static Notice.ViewModel.Command.NoticeCommand.LmsData2Command;
using static Notice.ViewModel.ViewModel;
using Excel = Microsoft.Office.Interop.Excel;

namespace Notice.ViewModel.Command.NoticeCommand
{
    /// <summary>
    /// 공지사항
    /// </summary>
    public class LmsData3Command : ICommand
	{
		int countBtn4 = 0; // 처음 실행이 아님을 확인
		int countExcel = 0;

		private System.Timers.Timer timer;

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		static Excel.Application excelApp = null;
		static Excel.Workbook workBook = null;
		static Excel.Worksheet workSheet = null;


		public event EventHandler CanExecuteChanged;

		public ViewModel VM { get; set; }
		public LmsData3Command(ViewModel vm)
		{
			VM = vm;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			timer = new System.Timers.Timer();
			timer.Interval = 1000 * 60;//한 시간
			timer.Elapsed += Timer_Elapsed;
			timer.AutoReset = true;
			timer.Enabled = true;
			timer.Start();

			VM.L_Data3.Add(new LmsData3()
			{
				LmsTitle3 = "데이터 로딩중"
			});
			VM.get3();
			VM.L_Data3.Clear();
			Start4();
			countBtn4++;
		}

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
			DispatcherService.Invoke((System.Action)(() =>
			{
				if (countBtn4 != 0)
				{
					VM.L_Data3_Main.Clear();
					VM.L_Data3.Clear();
				}
				VM.L_Data3.Add(new LmsData3()
				{
					LmsTitle3 = "데이터 로딩중"
				});
				VM.get3();
				VM.L_Data3.Clear();
				Start4();
				countBtn4++;
			}));
		}

        private async void Start4()
		{
			var task4 = Task.Run(() => NoticeCrawling());
			await task4;
			VM.get3();

			if (countExcel == 0) { saveExcel(); countExcel++; }
			else
			{
				//VM.E_Data3.Clear();
				readExcel();
				compareData();
				saveExcel();
			}
		}

		public void NoticeCrawling()
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

			element = _driver.FindElementByXPath("//*[@id='nav']/li[11]/a");
			element.Click();
            try
            {
				for (int i=2; i< _driver.FindElements(By.XPath("//*[@id='treebox']/div/table/tbody/tr")).Count; i++)
                {
					string BASE_Path = "//*[@id='treebox']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					Thread.Sleep(300);
					TextUpLoad4();
				}
			}catch(Exception ex)
            {
				return;
            }
			_driver.Close();
		}

		public void TextUpLoad4()
		{

			if (_driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]").Text == "2021-1학기 " + _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9) + "강의에게 공지할 내용이 없습니다.")
			{
				VM.L_Data3.Add(new LmsData3()
				{
					LmsSubject3 = _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9),
					LmsTitle3 = "업로드된 공지가 없습니다.",
					LmsRdate3 = "빔"
				});
			}
			else
			{
				VM.L_Data3.Add(new LmsData3()
				{
					LmsSubject3 = _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9),
					LmsTitle3 = _driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]/td[2]").Text,
					LmsRdate3 = _driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]/td[3]").Text,
				});
			}
		}

		public void saveExcel()
		{
			try
			{
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				string path3 = System.IO.Path.Combine(desktopPath, "공지사항.xlsx");

				excelApp = new Excel.Application();
				excelApp.DisplayAlerts = false;
				workBook = excelApp.Workbooks.Add();
				workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

				workSheet.Cells[1, 1] = "과 목";
				workSheet.Cells[1, 2] = "제 목";
				workSheet.Cells[1, 3] = "작성일";

				for (int i = 0; i < VM.getCount3(); i++)
				{
					workSheet.Cells[2 + i, 1] = VM.getList3().ElementAt(i).LmsSubject3;
					workSheet.Cells[2 + i, 2] = VM.getList3().ElementAt(i).LmsTitle3;
					workSheet.Cells[2 + i, 3] = VM.getList3().ElementAt(i).LmsRdate3;
				}
				workSheet.Columns.AutoFit();
				workSheet.SaveAs(path3, Excel.XlFileFormat.xlWorkbookDefault);
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
				string path = System.IO.Path.Combine(desktopPath, "공지사항.xlsx");
				workBook = excelApp.Workbooks.Open(path);
				workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

				Excel.Range rng = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[9, 9]];
				object[,] data = rng.Value;

				//excelData에 기록.
				for (int r = 2; r <= data.GetLength(1); r++)
				{
					if(data[r,1] != null && data[r, 2] != null && data[r, 3] != null)
                    {
						VM.E_Data3.Add(new ExcelData3() { ELmsSubject3 = data[r, 1].ToString(), ELmsTitle3 = data[r, 2].ToString(), ELmsRdate3 = data[r, 3].ToString() });
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
			for (int i = 0; i < VM.getCount3(); i++)
			{
				var lmsData1_Title = VM.getList3().ElementAt(i).LmsTitle3;
				var excelData_Title = VM.E_Data3.ElementAt(i).ELmsTitle3;

				if (lmsData1_Title != excelData_Title)
				{
					if (KakaoData.userToken != null)
					{
						//오류 있음 내용이 전송이 안됨
						VM.kakaoManager.KakaoDefaultSendMessage(VM.getList3().ElementAt(i).LmsSubject3 + "의 내용이 다릅니다.(마지막 비교와 비교해서 새 공지가 업로드 되었습니다.)");
                    }
                    else
                    {
						MessageBox.Show(VM.getList3().ElementAt(i).LmsSubject3 + "의 내용이 다릅니다.(마지막 비교와 비교해서 새 공지가 업로드 되었습니다.)");
                    }
					
				}
			}
		}
		
	}
}
