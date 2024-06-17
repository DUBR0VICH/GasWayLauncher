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
using System.Data;
using System.Data.SqlClient;
using GasWayLauncher.Classes;
using GasWayLauncher.ViewModel;

namespace GasWayLauncher.View
{
    public partial class LoginForm : Window
    {
        private RegisterForm registerForm;
        
        DataBase database = new DataBase();

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


        //Код для логина

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string loginUser = tb1.Text;
            string passwordUser = tb2.Password;

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            // Создаем строку запроса с использованием параметров
            string queryString = "SELECT Id, UserName, Password FROM UserInformation WHERE UserName = @username AND Password = @password";

            // Создаем команду SQL с параметрами
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            command.Parameters.AddWithValue("@username", loginUser);
            command.Parameters.AddWithValue("@password", passwordUser);

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count == 1)
            {
                MainWindow mainWindow = new MainWindow();
                if (mainWindow.DataContext is MainViewModel viewModel)
                {
                    viewModel.LoggedInUser = loginUser; // Передаем логин в ViewModel
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
