using Notice.Model;
using Notice.View;
using Notice.ViewModel.Command;
using Notice.ViewModel.Command.Binding;
using Notice.ViewModel.Command.FirstPageCommand;
using Notice.ViewModel.Command.HomePageCommand;
using Notice.ViewModel.Command.LoginPageCommand;
using Notice.ViewModel.Command.NoticeCommand;
using Notice.ViewModel.KakaoApi;
using Notice.ViewModel.NaverMail;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Notice.ViewModel
{
    public class ViewModel
    {
        /// <summary>
        /// HomePageCommand
        /// </summary>
        public List<DepartmentData> D_Data { get; set; }
        public ObservableCollection<DepartmentData> D_Data_Main { get; set; }
        public DepartmentDataButtonCommand departmentDataButtonCommand { get; set; }

        /// <summary>
        /// LoginPageCommand
        /// </summary>
        public MainWindowIDInformation mainWindowIDInformation { get; set; }
        public exitButtonCommand exitButtonCommand { get; set; }
        public signinButtonCommand loginButtonCommand { get; set; }
        //Test
        public StartBtnCmd StartBtnCmd { get; set; }
        public static MainWindow mainWindow = System.Windows.Application.Current.MainWindow as MainWindow;

        /// <summary>
        /// FirstPageCommnad
        /// </summary>
        public listViewSelected listViewSelected { get; set; }

        /// <summary>
        /// BindingPageCommand
        /// </summary>
        public BindingButton bindingButton { get; set; }
        public KakaoLogin kakaoLogin { get; set; }
        public KakaoLogout kakaoLogout { get; set; }
        public KakaoManager kakaoManager { get; set; }
        
        public NaverBinding naverBinding { get; set; }


        /// </summary>
        ///Notice Page Command
        /// </summary>

        /// <summary>
        /// KakaoApi, naver
        /// </summary>
        public KakaoData kakaoData { get; set; }
        public NaverData naverData { get; set; }
        public NaverMailManager naverMailManager { get; set; }


        public List<LmsData1> L_Data1 { get; set; }
        public List<LmsData2> L_Data2 { get; set; }
        public List<LmsData3> L_Data3 { get; set; }
        public ObservableCollection<LmsData1> L_Data1_Main { get; set; }
        public ObservableCollection<LmsData2> L_Data2_Main { get; set; }
        public ObservableCollection<LmsData3> L_Data3_Main { get; set; }
        public LmsData1Command lmsData1Command { get; set; }
        public LmsData2Command lmsData2Command { get; set; }
        public LmsData3Command lmsData3Command { get; set; }
        public List<ExcelData> E_Data { get; set; }
        public List<ExcelData2> E_Data2 { get; set; }
        public List<ExcelData3> E_Data3 { get; set; }
        
        public ViewModel()
        {

            D_Data = new List<DepartmentData>();
            D_Data_Main = new ObservableCollection<DepartmentData>();



            mainWindowIDInformation = new MainWindowIDInformation();
            exitButtonCommand = new exitButtonCommand();
            loginButtonCommand = new signinButtonCommand(this);
            bindingButton = new BindingButton();
            //Test
            StartBtnCmd = new StartBtnCmd();
            

            departmentDataButtonCommand = new DepartmentDataButtonCommand(this);
            listViewSelected = new listViewSelected();

            L_Data1 = new List<LmsData1>();
            L_Data1_Main = new ObservableCollection<LmsData1>();

            L_Data2 = new List<LmsData2>();
            L_Data2_Main = new ObservableCollection<LmsData2>();

            L_Data3 = new List<LmsData3>();
            L_Data3_Main = new ObservableCollection<LmsData3>();

            lmsData1Command = new LmsData1Command(this);
            lmsData2Command = new LmsData2Command(this);
            lmsData3Command = new LmsData3Command(this);
            
            E_Data = new List<ExcelData>();
            E_Data2 = new List<ExcelData2>();
            E_Data3 = new List<ExcelData3>();

            kakaoData = new KakaoData();
            kakaoManager = new KakaoManager();
            kakaoLogin = new KakaoLogin();


            naverBinding = new NaverBinding();
            naverMailManager = new NaverMailManager();
        }

        public static class DispatcherService
        {
            public static void Invoke(Action action)
            {
                Dispatcher dispatchObject = Application.Current != null ? Application.Current.Dispatcher : null;
                if (dispatchObject == null || dispatchObject.CheckAccess())
                    action();
                else
                    dispatchObject.Invoke(action);
            }
        }
        public void get1()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                L_Data1_Main.Clear();
                for (int i = 0; i < L_Data1.Count(); i++)
                {
                    L_Data1_Main.Add(L_Data1[i]);
                }
            }));
            
            
        }
        public void get2()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                L_Data2_Main.Clear();
                for (int i = 0; i < L_Data2.Count(); i++)
                {
                    L_Data2_Main.Add(L_Data2[i]);
                }
            }));
            
        }
        public void get3()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                L_Data3_Main.Clear();
                for (int i = 0; i < L_Data3.Count(); i++)
                {
                    L_Data3_Main.Add(L_Data3[i]);
                }
            }));

        }
        public void get4()
        {
            DispatcherService.Invoke((System.Action)(() =>
            {
                D_Data_Main.Clear();
                for (int i = 0; i < D_Data.Count(); i++)
                {
                    D_Data_Main.Add(D_Data[i]);
                }
            }));
            
        }

        //List 개수 얻기
        public int getCount1() { return L_Data1.Count(); }
        public int getCount2() { return L_Data2.Count(); }
        public int getCount3() { return L_Data3.Count(); }

        //List 접근하기
        public List<LmsData1> getList1() { return L_Data1; }
        public List<LmsData2> getList2() { return L_Data2; }
        public List<LmsData3> getList3() { return L_Data3; }

    }
}
