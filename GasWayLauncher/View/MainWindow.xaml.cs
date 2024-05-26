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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using GasWayLauncher.Classes;

namespace GasWayLauncher.View
{
    public partial class MainWindow : Window
    {


        private LoginForm loginForm;
        public MainWindow()
        {
            InitializeComponent();
        }

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


        //Профиль
        
    }
}
