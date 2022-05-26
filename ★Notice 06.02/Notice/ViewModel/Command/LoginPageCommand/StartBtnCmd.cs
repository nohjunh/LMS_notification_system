using Notice.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Notice.ViewModel.Command.LoginPageCommand
{
    public class StartBtnCmd : ICommand
    {
        public ViewModel VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            First first = new First();
            View.Home home = new View.Home();
            first.Show();
            ViewModel.mainWindow.Close();
            
            first.pageControl.NavigationService.Navigate(home);
        }
    }
}
