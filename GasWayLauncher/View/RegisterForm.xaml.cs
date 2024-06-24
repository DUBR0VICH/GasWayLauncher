using System.Linq;
using System.Windows;
using GasWayLauncher.Classes;
using GasWayLauncher.Model;
using System.Windows.Input;

namespace GasWayLauncher.View
{
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Toolbar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void LogoContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb2.Password.Length > 0)
            {
                Watermark.Visibility = Visibility.Collapsed;
            }
            else
            {
                Watermark.Visibility = Visibility.Visible;
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            var login = tb1.Text;
            var password = tb2.Password;

            var hashedPassword = PasswordHelper.HashPassword(password);

            using (var context = new ContextBD())
            {
                if (context.UserInfo.Any(u => u.UserName == login))
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                }
                else
                {
                    var user = new UserInformation { UserName = login, Password = hashedPassword };
                    context.UserInfo.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                    this.Close();
                }
            }
        }
    }
}
