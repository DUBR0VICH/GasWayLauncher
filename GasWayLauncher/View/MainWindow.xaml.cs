using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GasWayLauncher.ViewModel;

namespace GasWayLauncher.View
{
    public partial class MainWindow : Window
    {
        private LoginForm loginForm;
        public MainWindow()
        {
            InitializeComponent();
        }


        //Переход по ссылке
        private void OpenLink_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://dubrovich.pythonanywhere.com";
            OpenLink(url);
        }
        private void OpenLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть ссылку: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Далее всё остальное
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OpenLoginForm_Click(object sender, RoutedEventArgs e)
        {
            if (loginForm == null)
            {
                loginForm = new LoginForm();
                loginForm.Closed += (s, args) => loginForm = null;
                loginForm.Show();
                this.Close();
            }
            else
            {
                if (loginForm.WindowState == WindowState.Minimized)
                {
                    loginForm.WindowState = WindowState.Normal;
                    loginForm.Activate();
                }
                else loginForm.Activate();
            }
        }

        
    }
}
