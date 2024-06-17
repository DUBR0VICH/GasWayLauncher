using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GasWayLauncher.ViewModel;

namespace GasWayLauncher.View
{
    public partial class CommentsPage : Page, IUserPage
    {
        public string nickName;
        public CommentsPage()
        {
            InitializeComponent();
        }
        public void SetUser(string user)
        {
            nickName = user;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string newCommentText = CommentText.Text;

            if (!string.IsNullOrEmpty(newCommentText))
            {
                myScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                

                TextBlock newComment = new TextBlock
                {
                    Text = nickName + ": " + newCommentText,
                    Margin = new Thickness(100, 10, 0, 10),
                    Background = new SolidColorBrush(Color.FromArgb(
                        150,
                        0,
                        0,
                        0)),
                    Foreground = new SolidColorBrush(Color.FromArgb(
                        255,
                        61,
                        206,
                        0)),
                    FontSize = 14,
                    Padding = new Thickness(10),
                    Height = Double.NaN,
                    Width = 400,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    TextWrapping = TextWrapping.Wrap
                };
                
                CommentsPanel.Children.Add(newComment);
                myScrollViewer.Content = CommentsPanel;
                myScrollViewer.ScrollToEnd();
                CommentText.Clear();
            }
            else
            {
                MessageBox.Show("Комментарий не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
