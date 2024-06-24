using Xunit;
using Moq;
using System.Linq;
using GasWayLauncher.Model;
using GasWayLauncher.View;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using System.Windows.Media;

public class CommentsPageTests
{
    private DbContextOptions<ContextBD> CreateNewContextOptions()
    {
        var builder = new DbContextOptionsBuilder<ContextBD>();
        builder.UseInMemoryDatabase("TestDatabase");
        return builder.Options;
    }

    [Fact]
    public void AddCommentToDatabase_ShouldAddComment()
    {
        // Arrange
        var options = CreateNewContextOptions();

        using (var context = new ContextBD(options))
        {
            context.UserInfo.Add(new UserInformation { UserName = "TestUser", Password = "password" });
            context.SaveChanges();
        }

        var commentsPage = new CommentsPage { NickName = "TestUser" };

        // Act
        commentsPage.AddCommentToDatabase("Test comment");

        using (var context = new ContextBD(options))
        {
            // Assert
            Assert.Equal(1, context.UserMess.Count());
            Assert.Equal("Test comment", context.UserMess.First().message);
        }
    }

    [Fact]
    public void LoadComments_ShouldLoadComments()
    {
        // Arrange
        var options = CreateNewContextOptions();

        using (var context = new ContextBD(options))
        {
            var user = new UserInformation { UserName = "TestUser", Password = "password" };
            context.UserInfo.Add(user);
            context.UserMess.Add(new UserMessages { username_id = user.Id, message = "Test comment" });
            context.SaveChanges();
        }

        var commentsPage = new CommentsPage { NickName = "TestUser" };

        // Act
        commentsPage.LoadComments();

        // Assert
        Assert.Single(commentsPage.CommentsPanel.Children);
        var textBlock = commentsPage.CommentsPanel.Children[0] as TextBlock;
        Assert.Equal("TestUser: Test comment", textBlock.Text);
    }
}
