using System.Diagnostics;
using System.Windows;

namespace Notice.View
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
                var home = new View.Home();
                pageControl.NavigationService.Navigate(home);
            }
            if (sender == CalenadarItem)
            {
                var calenadar = new View.Calendar();
                pageControl.NavigationService.Navigate(calenadar);
            }
            if (sender == NoticeItem)
            {
                var notice = new View.Notice();
                pageControl.NavigationService.Navigate(notice);
            }
            if (sender == BindingItem)
            {
                var binding = new View.Binding();
                pageControl.NavigationService.Navigate(binding);
            }
            if (sender == SignOutItem)
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
}
