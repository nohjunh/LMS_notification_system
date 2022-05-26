using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Notice.ViewModel.Command
{
    public class exitButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Process[] processList = Process.GetProcessesByName("chromedriver");
            for (int i = processList.Length - 1; i >= 0; i--)
            {
                // processList[i].CloseMainWindow();
                processList[i].Kill();
                processList[i].Close();
            }

            Application.Current.Shutdown();
        }
    }
}
