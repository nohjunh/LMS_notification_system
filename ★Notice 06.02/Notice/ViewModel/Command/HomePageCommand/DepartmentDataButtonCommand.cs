using Notice.Model;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notice.ViewModel.Command.HomePageCommand
{
    public class DepartmentDataButtonCommand : ICommand
    {
        int countBtn5 = 0; // 처음 실행이 아님을 확인

		protected ChromeDriverService _driverService = null;
		protected ChromeOptions _options = null;
		protected ChromeDriver _driver = null;

		public event EventHandler CanExecuteChanged;

		public ViewModel VM { get; set; }

		public DepartmentDataButtonCommand(ViewModel vm)
		{
			VM = vm;
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			if (countBtn5 != 0)
			{
				VM.D_Data_Main.Clear();
				VM.D_Data.Clear();
			}
			VM.D_Data.Add(new DepartmentData()
			{
				D_Title = "            데이터 로딩중"
			});
			VM.get4();
			VM.D_Data.Clear();
			Start5();
			countBtn5++;
		}

		private async void Start5()
		{
			var task5 = Task.Run(() => SubjectCrawling());
			await task5;
			VM.get4();
		}
		private void SubjectCrawling()
		{
			_driverService = ChromeDriverService.CreateDefaultService();
			_driverService.HideCommandPromptWindow = true;
			_options = new ChromeOptions();
			_options.AddArgument("headless");
			_options.AddArgument("disable-gpu");
			_driver = new ChromeDriver(_driverService, _options);

			_driver.Navigate().GoToUrl("https://cse.jbnu.ac.kr/cse/3586/subview.do"); // 웹 사이트에 접속합니다.

			_driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			string BASE_Path1 = "//*[@id='menu3586_obj176']/div[2]/form[2]/table/tbody/tr[{0}]/";
			for (int i = 1; i < 15; i++)
			{
				string url1 = string.Format(BASE_Path1, i);
				string Base_value1 = url1;
				VM.D_Data.Add(new DepartmentData()
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