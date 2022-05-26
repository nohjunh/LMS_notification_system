using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer = new Timer();
            timer.Interval = 1000; // 1초 == 1000
            timer.Elapsed += Timer_Elapsed; //이벤트헨들러 등록
            timer.Start(); //타이머 시작
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //실행로직
            File.AppendAllText(@"C:\Users\구성재\Desktop\test.txt", "안녕하세요\n");
        }

        protected override void OnStop()
        {
            timer.Stop();
        }
    }
}
