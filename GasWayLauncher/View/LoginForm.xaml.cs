using System.Linq;
using System.Windows;
using GasWayLauncher.Classes;
using System.Windows.Input;
using GasWayLauncher.Model;
using GasWayLauncher.ViewModel;

namespace GasWayLauncher.View
{
    public partial class LoginForm : Window
    {
        private RegisterForm registerForm;

        public static string loginUser { get; internal set; }

        public LoginForm()
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LeftGrid.Visibility == Visibility.Hidden)
                LeftGrid.Visibility = Visibility.Visible;
            else
                LeftGrid.Visibility = Visibility.Hidden;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (RightGrid.Visibility == Visibility.Hidden)
                RightGrid.Visibility = Visibility.Visible;
            else
                RightGrid.Visibility = Visibility.Hidden;
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

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (registerForm == null)
            {
                registerForm = new RegisterForm();
                registerForm.Closed += (s, args) => registerForm = null;
                registerForm.Show();
                this.Close();
            }
            else
            {
                if (registerForm.WindowState == WindowState.Minimized)
                {
                    registerForm.WindowState = WindowState.Normal;
                    registerForm.Activate();
                }
                else registerForm.Activate();
            }
        }

        public void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string loginUser = tb1.Text;
            string passwordUser = tb2.Password;

            var hashedPassword = PasswordHelper.HashPassword(passwordUser);

            using (var context = new ContextBD())
            {
                var user = context.UserInfo
                    .FirstOrDefault(u => u.UserName == loginUser && u.Password == hashedPassword);

                if (user != null)
                {
                    MainWindow mainWindow = new MainWindow();
                    if (mainWindow.DataContext is MainViewModel viewModel)
                    {
                        viewModel.LoggedInUser = loginUser;
                    }
                    mainWindow.AccTextBlock.Text = loginUser;
                    mainWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль!");
                }
            }
        }
    }
}
