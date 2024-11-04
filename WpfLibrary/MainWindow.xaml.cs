using Models;
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
using WpfLibrary.ViewModel;

namespace WpfLibrary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StaffViewModel _staffViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _staffViewModel = new StaffViewModel(); // Khởi tạo StaffViewModel
            DataContext = _staffViewModel;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var token = await _staffViewModel.Login(); // Thực hiện đăng nhập
            if (token != null)
            {
                MessageBox.Show("Login successful!");

                // Tạo và hiển thị cửa sổ Staff.xaml
                Staff staffWindow = new Staff(); // Giả định rằng bạn đã có Staff.xaml
                staffWindow.Show();

                // Đóng cửa sổ đăng nhập sau khi đăng nhập thành công
                this.Close();
            }
            else
            {
                MessageBox.Show("Login failed. Please check your credentials.");
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;
            _staffViewModel.SetPassword(passwordBox.Password);
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
