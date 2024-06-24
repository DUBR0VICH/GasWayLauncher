using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GasWayLauncher.Model;
using GasWayLauncher.ViewModel;

namespace GasWayLauncher.View
{
    public partial class CommentsPage : Page, IUserPage
    {
        public string NickName { get; set; }

        public CommentsPage()
        {
            InitializeComponent();
            LoadComments();
        }

        public void SetUser(string user)
        {
            NickName = user;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string newCommentText = CommentText.Text;

            if (!string.IsNullOrEmpty(newCommentText))
            {
                try
                {
                    AddCommentToDatabase(newCommentText);
                    CommentText.Clear();
                    LoadComments();
                    myScrollViewer.ScrollToEnd();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении комментария: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Комментарий не может быть пустым.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void AddCommentToDatabase(string comment)
        {
            using (var context = new ContextBD())
            {
                var user = context.UserInfo.FirstOrDefault(u => u.UserName == NickName);
                if (user != null)
                {
                    var newComment = new UserMessages
                    {
                        username_id = user.Id,
                        message = comment
                    };

                    context.UserMess.Add(newComment);
                    context.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void LoadComments()
        {
            CommentsPanel.Children.Clear();
            using (var context = new ContextBD())
            {
                var comments = from msg in context.UserMess
                               join user in context.UserInfo on msg.username_id equals user.Id
                               select new { user.UserName, msg.message };

                foreach (var comment in comments)
                {
                    var textBlock = new TextBlock
                    {
                        Text = $"{comment.UserName}: {comment.message}",
                        FontSize = 14,
                        Margin = new Thickness(5),
                        Foreground = new SolidColorBrush(Colors.Black)
                    };
                    CommentsPanel.Children.Add(textBlock);
                }
            }
        }

        private void CommentText_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CommentText.Text == "Введите комментарий...")
            {
                CommentText.Text = string.Empty;
                CommentText.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void CommentText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CommentText.Text))
            {
                CommentText.Text = "Введите комментарий...";
                CommentText.Foreground = new SolidColorBrush(Colors.Gray);
            }
        }
    }
}
