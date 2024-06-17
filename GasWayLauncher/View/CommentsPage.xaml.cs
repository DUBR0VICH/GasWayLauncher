using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using GasWayLauncher.Classes;
using GasWayLauncher.ViewModel;

namespace GasWayLauncher.View
{
    public partial class CommentsPage : Page, IUserPage
    {
        public string nickName;
        private DataBase database;

        public CommentsPage()
        {
            InitializeComponent();
            database = new DataBase();
            LoadComments();
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


        private void AddCommentToDatabase(string comment)
        {
            try
            {
                // Получаем Id пользователя по имени пользователя
                string queryString = "SELECT Id FROM UserInformation WHERE UserName = @username";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                command.Parameters.AddWithValue("@username", nickName);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    int userId = (int)table.Rows[0]["Id"];
                    // Вставляем комментарий в базу данных
                    string insertQuery = "INSERT INTO UserMessages (username_id, message) VALUES (@username_id, @message)";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, database.getConnection());
                    insertCommand.Parameters.AddWithValue("@username_id", userId);
                    insertCommand.Parameters.AddWithValue("@message", comment);

                    database.openConnection();
                    insertCommand.ExecuteNonQuery();
                    database.closeConnection();
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении комментария: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LoadComments()
        {
            CommentsPanel.Children.Clear();
            try
            {
                string queryString = @"
            SELECT UserInformation.UserName, UserMessages.message 
            FROM UserMessages
            JOIN UserInformation ON UserMessages.username_id = UserInformation.Id";
                SqlCommand command = new SqlCommand(queryString, database.getConnection());

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                foreach (DataRow row in table.Rows)
                {
                    string user = row["UserName"].ToString();
                    string message = row["message"].ToString();
                    AddCommentToPanel(user, message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке комментариев: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void AddCommentToPanel(string user, string comment)
        {
            TextBlock newComment = new TextBlock
            {
                Text = user + ": " + comment,
                Margin = new Thickness(100, 10, 0, 10),
                Background = new SolidColorBrush(Color.FromArgb(150, 0, 0, 0)),
                Foreground = new SolidColorBrush(Color.FromArgb(255, 61, 206, 0)),
                FontSize = 14,
                Padding = new Thickness(10),
                Height = Double.NaN,
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                TextWrapping = TextWrapping.Wrap
            };

            CommentsPanel.Children.Add(newComment);
        }
    }
}
