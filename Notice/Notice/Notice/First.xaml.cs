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
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace Notice
{
    public partial class First : Window
    {
        public First()
        {
            InitializeComponent();
        }

        //메뉴 가장 상단의 삼선 버튼 변화 기능
        private void OpenMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Collapsed;
            CloseMenuButton.Visibility = Visibility.Visible;
        }
        private void CloseMenuButton_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuButton.Visibility = Visibility.Visible;
            CloseMenuButton.Visibility = Visibility.Collapsed;
        }

        //ListViewItem 클릭시 Page 전환 기능 
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            if (sender == HomeItem) 
            {
                var home = new Pages.Home();
                pageControl.NavigationService.Navigate(home);
            }
            if (sender == CalenadarItem)
            {
                var calenadar = new Pages.Calendar();
                pageControl.NavigationService.Navigate(calenadar);
            }
            if (sender == NoticeItem)
            {
                var notice = new Pages.Notice();
                pageControl.NavigationService.Navigate(notice);
            }
            if (sender == BindingItem)
            {
                var binding = new Pages.Binding();
                pageControl.NavigationService.Navigate(binding);
            }
            if (sender == SignOutItem)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
