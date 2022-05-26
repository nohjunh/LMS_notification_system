using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using crawling.Classes;
//Selenium Library
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
//excel
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;



namespace crawling
{
	public enum WhatButton //어떤 버튼이 클릭되었는 지에 따라서 저장해야하는 excel 파일이 다르게 하기 위해 배열 생성
    {
		button2, button3, button4, button5
    }

	public partial class MainWindow : Window
	{
		List<DepartmentData> D_Data = new List<DepartmentData>();
		List<LmsData> L_Data = new List<LmsData>();
		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		int countBtn1 = 0; // 처음 실행이 아님을 확인
		int countBtn2 = 0; // 처음 실행이 아님을 확인
		

		static string id;
		static string pw;
		static int grade;

		//엑셀 앱관련  필드
		static Excel.Application excelApp = null;
		static Excel.Workbook workBook = null;
		static Excel.Worksheet workSheet = null;
		WhatButton whatButton;//배열 생성 

		public MainWindow()
		{
			InitializeComponent();

			button1.Click += button1_Initialized;
			button2.Click += button2_Initialized;
			button3.Click += button3_Initialized;
			button4.Click += button4_Initialized;
			button5.Click += button5_Initialized;

			_driverService = ChromeDriverService.CreateDefaultService();
			_driverService.HideCommandPromptWindow = true;

			_options = new ChromeOptions();
		    _options.AddArgument("headless");
			_options.AddArgument("disable-gpu");

			this.DataContext = new LoginViewModel();

		}


		// ------------------LMS로그인 확인--------------------------
		public void button1_Initialized(object sender, EventArgs e)

		{
			var viewModel = this.DataContext as LoginViewModel;

			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement element;
			try
			{
				element = _driver.FindElementByXPath("//*[@id='id']");
				element.SendKeys(viewModel.LoginID);
				id = viewModel.LoginID;

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(viewModel.LoginPasswd);
				pw = (viewModel.LoginPasswd);

				element = _driver.FindElementByXPath("//*[@id='loginform']/table/tbody/tr[1]/td[2]/input");
				element.Click();

				element = _driver.FindElementByXPath("//*[@id='nav']/li[10]/a");
				element.Click();

				MessageBox.Show("로그인 성공! 원하는 메뉴를 클릭해주세요");

				_driver.Close();
			}
			catch (Exception)
			{
				MessageBox.Show("ID,PW를 확인해주세요.");
				return;
			}
		}


		// ------------------강의자료 긁어오기--------------------------
		public void button2_Initialized(object sender, EventArgs e)
		{
			whatButton = WhatButton.button2;//button2가 기록됨
			if (countBtn1 != 0)
			{
				L_Data.Clear();
			}
			foreach (CheckBox cbx in stp.Children.OfType<CheckBox>())
			{

			      if (cbx.Content.ToString() == "21.5학점(대진설O)")
			      {
					grade = 21;
				  }
				  if (cbx.Content.ToString() == "18.5학점(대진설O)")
				  {
					grade = 18;
				  }
				  if (cbx.Content.ToString() == "15.5학점(대진설O)")
				  {
				 	grade = 15;
				  }
			}
			Start2();
			countBtn1++;
		}
		private async void Start2()
		{
			var task2 = Task.Run(() => DataCrawling());
			await task2;
			Lms2CrawlingData.ItemsSource = L_Data;
			readExcel(); //반드시 save보다 read를 먼저 해야한다!!!!!!!
			saveAsExcel();
		}

		public void DataCrawling()
		{
			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement element;
			try
			{
				element = _driver.FindElementByXPath("//*[@id='id']");
				element.SendKeys(id);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(pw);

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

			element = _driver.FindElementByXPath("//*[@id='nav']/li[3]/a");
			element.Click();

				if(grade == 21)
			    { 
					for (int i = 2; i < 10; i++)
					{
						element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
						element.Click();
						string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
						string url = string.Format(BASE_Path, i);
						string BASE_value = url;
						element = _driver.FindElementByXPath(BASE_value);
						element.Click();
						TextUpLoad2();
					}
				}
				if (grade == 18)
				{
					for (int i = 2; i < 9; i++)
					{
						element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
						element.Click();
						string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
						string url = string.Format(BASE_Path, i);
						string BASE_value = url;
						element = _driver.FindElementByXPath(BASE_value);
						element.Click();
						TextUpLoad2();
					}
				}
				if (grade == 15)
				{
					for (int i = 2; i < 8; i++)
					{
						element = _driver.FindElementByXPath("//*[@id='center']/div/div[2]/div/div[3]/a/span");
						element.Click();
						string BASE_Path = "//*[@id='treeboxtab']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
						string url = string.Format(BASE_Path, i);
						string BASE_value = url;
						element = _driver.FindElementByXPath(BASE_value);
						element.Click();
						TextUpLoad2();
					}
				}
			_driver.Close();
		}

		public void TextUpLoad2()
		{
			if (_driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr").Text == "해당하는 자료 정보가 없습니다.")
			{
				L_Data.Add(new LmsData()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = "업로드된 자료가 없습니다.",
					LmsRdate ="nothing"
				});
				return;
			}
			else
			{
				L_Data.Add(new LmsData()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = _driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr[1]/td[2]").Text,
					LmsRdate = _driver.FindElementByXPath("//*[@id='borderB']/tbody[2]/tr[1]/td[4]").Text,

				});
			}
		}

		// ------------------강의레포트 긁어오기--------------------------

		public void button3_Initialized(object sender, EventArgs e)
		{
			whatButton = WhatButton.button3;//button3가 기록됨
			if (countBtn1 != 0)
			{
				L_Data.Clear();
			}
			foreach (CheckBox cbx in stp.Children.OfType<CheckBox>())
			{

				if (cbx.Content.ToString() == "21.5학점(대진설O)")
				{
					grade = 21;
				}
				if (cbx.Content.ToString() == "18.5학점(대진설O)")
				{
					grade = 18;
				}
				if (cbx.Content.ToString() == "15.5학점(대진설O)")
				{
					grade = 15;
				}
			}

			Start3();
			countBtn1++;
		}
		private async void Start3()
		{
			var task3 = Task.Run(() => ReportCrawling());
			await task3;
			Lms3CrawlingData.ItemsSource = L_Data;
			saveAsExcel();

		}

		public void ReportCrawling()
		{
			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement element;
			try
			{
				element = _driver.FindElementByXPath("//*[@id='id']");
				element.SendKeys(id);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(pw);

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

			if (grade == 21)
			{
				for (int i = 2; i < 10; i++)
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
			}
			if (grade == 18)
			{
				for (int i = 2; i < 9; i++)
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
			}
			if (grade == 15)
			{
				for (int i = 2; i < 8; i++)
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
			}
			_driver.Close();
		}
	
		public void TextUpLoad3()
		{
			if(_driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]").Text == "해당하는 레포트 정보가 없습니다.")
            {
				L_Data.Add(new LmsData()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = "업로드된 레포트가 없습니다."
				});
				return;
			}
            else
            {
				L_Data.Add(new LmsData()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[2]").Text,
					LmsRdate = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[6]").Text

				});
			}
			/*
			L_Data.Add(new LmsData()
			{
				LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
				LmsTitle = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]").Text
		});
			*/
			//var tex3 = _driver.FindElement(By.XPath("//*[@id='borderB']/tbody/tr[2]"));
			//Lms3CrawlingData.Items.Add(tex3.Text);
		}


		// ------------------강의공지 긁어오기--------------------------
		public void button4_Initialized(object sender, EventArgs e)
		{
			whatButton = WhatButton.button4;//button4가 기록됨
			if (countBtn1 != 0)
			{
				L_Data.Clear();
			}
			foreach (CheckBox cbx in stp.Children.OfType<CheckBox>())
			{

				if (cbx.Content.ToString() == "21.5학점(대진설O)")
				{
					grade = 21;
				}
				if (cbx.Content.ToString() == "18.5학점(대진설O)")
				{
					grade = 18;
				}
				if (cbx.Content.ToString() == "15.5학점(대진설O)")
				{
					grade = 15;
				}
			}

			Start4();
			countBtn1++;
		}
		private async void Start4()
        {
			var task4 = Task.Run(() => NoticeCrawling());
			await task4;
			Lms4CrawlingData.ItemsSource = L_Data;
			saveAsExcel();

		}

		public void NoticeCrawling()
		{

			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://ieilms.jbnu.ac.kr/"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement element;
			try
			{
				element = _driver.FindElementByXPath("//*[@id='id']");
				element.SendKeys(id);

				element = _driver.FindElementByXPath("//*[@id='passwd']");
				element.SendKeys(pw);

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

			element = _driver.FindElementByXPath("//*[@id='nav']/li[10]/a");
			element.Click();

			if (grade == 21)
			{
				for (int i = 2; i < 10; i++)
				{
					string BASE_Path = "//*[@id='treebox']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
					string url = string.Format(BASE_Path, i);
					string BASE_value = url;
					element = _driver.FindElementByXPath(BASE_value);
					element.Click();
					Thread.Sleep(300);
					TextUpLoad4();
				}
			}
			if (grade == 18)
			{
				for (int i = 2; i < 9; i++)
				{
								string BASE_Path = "//*[@id='treebox']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
								string url = string.Format(BASE_Path, i);
								string BASE_value = url;
								element = _driver.FindElementByXPath(BASE_value);
								element.Click();
								Thread.Sleep(300);
								TextUpLoad4();
				}
			}
			if (grade == 15)
			{
				for (int i = 2; i < 8; i++)
				{
								string BASE_Path = "//*[@id='treebox']/div/table/tbody/tr[{0}]/td[2]/table/tbody/tr/td[4]/span";
								string url = string.Format(BASE_Path, i);
								string BASE_value = url;
								element = _driver.FindElementByXPath(BASE_value);
								element.Click();
								Thread.Sleep(300);
								TextUpLoad4();
				}
			}
			_driver.Close();
		}

		public void TextUpLoad4()
		{

			if (_driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]").Text == "2021-1학기 " + _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9) + "강의에게 공지할 내용이 없습니다.")
            {
				L_Data.Add(new LmsData()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9),
					LmsTitle = "업로드된 공지가 없습니다."
				});
			}
            else
            {
				L_Data.Add(new LmsData()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9),
					LmsTitle = _driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]/td[2]").Text,
					LmsRdate = _driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]/td[3]").Text,

				});
			}
		}


		// ------------------학과공지 긁어오기--------------------------
		private void button5_Initialized(object sender, EventArgs e)
		{
			whatButton = WhatButton.button5;//button5가 기록됨
			if (countBtn2 != 0)
			{
				D_Data.Clear();
			}
			Start5();
			countBtn2++;
		}

		private async void Start5()
		{
			var task5 = Task.Run(() => SubjectCrawling());
			await task5;
			DepartmentCrawlingData.ItemsSource = D_Data;
			saveAsExcel();
		}
		private void SubjectCrawling()
		{
			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://cse.jbnu.ac.kr/cse/3586/subview.do"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			string BASE_Path1 = "//*[@id='menu3586_obj176']/div[2]/form[2]/table/tbody/tr[{0}]/";
			for (int i = 1; i < 15; i++)
			{
				string url1 = string.Format(BASE_Path1, i);
				string Base_value1 = url1;
				D_Data.Add(new DepartmentData()
				{
					D_Num = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdNum']").Text,
					D_Title = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdTitle']").Text,
					D_Writer = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdWriter']").Text,
					D_Rdate = _driver.FindElementByXPath(Base_value1 + "td[@class='_artclTdRdate']").Text

				});
			}
			_driver.Close();
		}

		//엑셀 파일 옮기기
		public void saveAsExcel()
		{
			try
			{
				string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				switch (whatButton)
                {
					case WhatButton.button2:
						string path2 = System.IO.Path.Combine(desktopPath, "강의자료.xlsx");

						excelApp = new Excel.Application();
						workBook = excelApp.Workbooks.Add();
						workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

						workSheet.Cells[1, 1] = c1.Header.ToString();
						workSheet.Cells[1, 2] = c2.Header.ToString();
						workSheet.Cells[1, 3] = c3.Header.ToString();

						for (int i = 0; i < Lms2CrawlingData.Items.Count; i++)
						{

							workSheet.Cells[2 + i, 1] = L_Data.ElementAt(i).LmsSubject;
							workSheet.Cells[2 + i, 2] = L_Data.ElementAt(i).LmsTitle;
							workSheet.Cells[2 + i, 3] = L_Data.ElementAt(i).LmsRdate;
						}
						workSheet.Columns.AutoFit();
						workSheet.SaveAs(path2, Excel.XlFileFormat.xlWorkbookDefault);
						workBook.Close(true);
						excelApp.Quit();
						break;

					case WhatButton.button3:
						string path3 = System.IO.Path.Combine(desktopPath, "강의레포트.xlsx");

						excelApp = new Excel.Application();
						workBook = excelApp.Workbooks.Add();
						workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

						workSheet.Cells[1, 1] = c1.Header.ToString();
						workSheet.Cells[1, 2] = c2.Header.ToString();
						workSheet.Cells[1, 3] = c3.Header.ToString();

						for (int i = 0; i < Lms2CrawlingData.Items.Count; i++)
						{

							workSheet.Cells[2 + i, 1] = L_Data.ElementAt(i).LmsSubject;
							workSheet.Cells[2 + i, 2] = L_Data.ElementAt(i).LmsTitle;
							workSheet.Cells[2 + i, 3] = L_Data.ElementAt(i).LmsRdate;
						}
						workSheet.Columns.AutoFit();
						workSheet.SaveAs(path3, Excel.XlFileFormat.xlWorkbookDefault);
						workBook.Close(true);
						excelApp.Quit();
						break;

					case WhatButton.button4:
						string path4 = System.IO.Path.Combine(desktopPath, "강의공지.xlsx");
						excelApp = new Excel.Application();
						workBook = excelApp.Workbooks.Add();
						workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

						workSheet.Cells[1, 1] = c1.Header.ToString();
						workSheet.Cells[1, 2] = c2.Header.ToString();
						workSheet.Cells[1, 3] = c3.Header.ToString();

						for (int i = 0; i < Lms2CrawlingData.Items.Count; i++)
						{

							workSheet.Cells[2 + i, 1] = L_Data.ElementAt(i).LmsSubject;
							workSheet.Cells[2 + i, 2] = L_Data.ElementAt(i).LmsTitle;
							workSheet.Cells[2 + i, 3] = L_Data.ElementAt(i).LmsRdate;
						}
						workSheet.Columns.AutoFit();
						workSheet.SaveAs(path4, Excel.XlFileFormat.xlWorkbookDefault);
						workBook.Close(true);
						excelApp.Quit();
						break;
					case WhatButton.button5:
						string path5 = System.IO.Path.Combine(desktopPath, "학과공지.xlsx");

						excelApp = new Excel.Application();
						workBook = excelApp.Workbooks.Add();
						workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet;

						workSheet.Cells[1, 1] = majorC1.Header.ToString();
						workSheet.Cells[1, 2] = majorC2.Header.ToString();
						workSheet.Cells[1, 3] = majorC3.Header.ToString();
						workSheet.Cells[1, 4] = majorC4.Header.ToString();

						for (int i = 0; i < Lms2CrawlingData.Items.Count; i++)
						{

							workSheet.Cells[2 + i, 1] = D_Data.ElementAt(i).D_Num;
							workSheet.Cells[2 + i, 2] = D_Data.ElementAt(i).D_Title;
							workSheet.Cells[2 + i, 3] = D_Data.ElementAt(i).D_Writer;
							workSheet.Cells[2 + i, 4] = D_Data.ElementAt(i).D_Rdate;
						}
						workSheet.Columns.AutoFit();
						workSheet.SaveAs(path5, Excel.XlFileFormat.xlWorkbookDefault);
						workBook.Close(true);
						excelApp.Quit();
						break;
				}
			}
			finally
			{
				ReleaseObject(workSheet);
				ReleaseObject(workBook);
				ReleaseObject(excelApp);
			}
		}
        //엑셀 파일 읽어오기 -강의 자료에 대해서만 Test
        public void readExcel()
        {
            Excel.Application excelApp = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;

            try
            {
                excelApp = new Excel.Application();

                //엑셀 파일 열기
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string path = System.IO.Path.Combine(desktopPath, "강의자료.xlsx");
                wb = excelApp.Workbooks.Open(path);
                //첫 번째 Worksheet
                ws = wb.Worksheets.get_Item(1) as Excel.Worksheet;
                //현재 Worksheet에서 일부 범위만 선택 → 속도를 위해
                Excel.Range rng = ws.Range[ws.Cells[1, 1], ws.Cells[7, 7]];
				//Range 데이타를 배열 (One-based array)로
				object [,] data = rng.Value;


                //excelData에 기록.
                for (int r = 2; r <=data.GetLength(1); r++)
                {
					excelData.GetE_Data().Add(new excelData() { ELmsSubject = data[r, 1].ToString(), ELmsTitle = data[r, 2].ToString(), ELmsRdata = data[r, 3].ToString() });
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

		//강의자료의 제목 부분 값 비교하기(새 크롤링 데이터와 excel에 기록된 것 비교)
		public void compareData()
        {
			//해당 과목 Subject 이름을 넣어줌.
			List<string> CompareList = new List<string>();

			for(int i = 0; i<L_Data.Count;i++)
            {
				var lData_Title = L_Data.ElementAt(i).LmsTitle;
				var eData_Title = excelData.GetE_Data().ElementAt(i).ELmsTitle;

				if(lData_Title!=eData_Title)
                {
					CompareList.Add(lData_Title);
					MessageBox.Show(L_Data.ElementAt(i).LmsSubject + "의 내용이 다릅니다. (업로드 되었습니다.)");
                }
				else
                {
					CompareList.Add(lData_Title);
					MessageBox.Show(L_Data.ElementAt(i).LmsSubject + "의 내용이 같습니다.(업로드 되지 않았습니다.)");
                }
            }

        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
			compareData();
        }
    }
}
