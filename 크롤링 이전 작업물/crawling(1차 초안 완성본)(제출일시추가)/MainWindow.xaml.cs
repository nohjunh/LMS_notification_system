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



namespace crawling
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
		List<DepartmentData> D_Data = new List<DepartmentData>();
		List<LmsData1> L_Data1 = new List<LmsData1>();
		List<LmsData2> L_Data2 = new List<LmsData2>();
		List<LmsData3> L_Data3 = new List<LmsData3>();

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		int countBtn1 = 0; // 처음 실행이 아님을 확인
		int countBtn2 = 0; // 처음 실행이 아님을 확인
		int countBtn3 = 0; // 처음 실행이 아님을 확인
		int countBtn4 = 0; // 처음 실행이 아님을 확인
		int countBtn5 = 0; // 처음 실행이 아님을 확인

		static string id;
		static string pw;
		static int grade;

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
			if (countBtn2 != 0)
			{
				L_Data1.Clear();
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
			Lms2CrawlingData.ItemsSource = L_Data1;

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
				L_Data1.Add(new LmsData1()
				{
					LmsSubject = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle = "업로드된 자료가 없습니다."
				});
				return;
			}
			else
			{
				L_Data1.Add(new LmsData1()
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
			if (countBtn3 != 0)
			{
				L_Data2.Clear();
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
			Lms3CrawlingData.ItemsSource = L_Data2;

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
				L_Data2.Add(new LmsData2()
				{
					LmsSubject2 = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsTitle2 = "업로드된 레포트가 없습니다."
				});
				return;
			}
            else
            {
				L_Data2.Add(new LmsData2()
				{
					LmsSubject2 = _driver.FindElementByXPath("//*[@id='center']/div/div[1]/div[1]/div[1]").Text.Substring(9),
					LmsEndDate2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[3]").Text,
					LmsTitle2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[2]").Text,
					LmsRdate2 = _driver.FindElementByXPath("//*[@id='borderB']/tbody/tr[2]/td[6]").Text
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
			if (countBtn4 != 0)
			{
				L_Data3.Clear();
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
			Lms4CrawlingData.ItemsSource = L_Data3;

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
				L_Data3.Add(new LmsData3()
				{
					LmsSubject3 = _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9),
					LmsTitle3 = "업로드된 공지가 없습니다."
				});
			}
            else
            {
				L_Data3.Add(new LmsData3()
				{
					LmsSubject3 = _driver.FindElementByXPath("//*[@id='gname']").Text.Substring(9),
					LmsTitle3 = _driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]/td[2]").Text,
					LmsRdate3 = _driver.FindElementByXPath("//*[@id='board']/tbody/tr[2]/td[3]").Text,

				});
			}
		}


		// ------------------학과공지 긁어오기--------------------------
		private void button5_Initialized(object sender, EventArgs e)
		{
			if (countBtn5 != 0)
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
	}
}
