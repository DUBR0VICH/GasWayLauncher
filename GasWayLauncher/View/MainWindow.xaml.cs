using System;
using System.ComponentModel;
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
