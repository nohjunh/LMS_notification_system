using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


namespace Notice
{
    public partial class MainWindow : Window
    {
        //메인 윈도우
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            First first = new First();
            Pages.Home home = new Pages.Home();
            first.Show();
            this.Close();
            first.pageControl.NavigationService.Navigate(home);
        }
    }
}
