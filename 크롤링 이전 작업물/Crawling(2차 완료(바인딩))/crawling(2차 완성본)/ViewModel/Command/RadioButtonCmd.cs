using crawling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace crawling.ViewModel.Command
{
    class RadioButtonCmd : ICommand
    {
        public Grade GD { get; set; }
        public MainWindow main { get; set; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            if ((bool)main.학점21.IsChecked)
                GD.GradeNumber = 21;
            if ((bool)main.학점18.IsChecked)
                GD.GradeNumber = 18;
            if ((bool)main.학점21.IsChecked)
                GD.GradeNumber = 15;
        }
    }
}
