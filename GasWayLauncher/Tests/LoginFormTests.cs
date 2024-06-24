using Xunit;
using Moq;
using System.Linq;
using GasWayLauncher.Classes;
using GasWayLauncher.Model;
using GasWayLauncher.View;
using Microsoft.EntityFrameworkCore;

    public class LoginFormTests
{

    private DbContextOptions<ContextBD> CreateNewContextOptions()
    {
        var builder = new DbContextOptionsBuilder<ContextBD>();
        builder.UseInMemoryDatabase("TestDatabase");
        return builder.Options;
    }

    [Fact]
    public void Button_Click_2_ShouldLoginUser()
    {
        // Arrange
        var options = CreateNewContextOptions();

        using (var context = new ContextBD(options))
        {
            var hashedPassword = PasswordHelper.HashPassword("password123");
            context.UserInfo.Add(new UserInformation { UserName = "TestUser", Password = hashedPassword });
            context.SaveChanges();
        }

        var loginForm = new LoginForm();
        loginForm.tb1.Text = "TestUser";
        loginForm.tb2.Password = "password123";

        // Act
        loginForm.Button_Click_2(null, null);

        using (var context = new ContextBD(options))
        {
            // Assert
            Assert.NotNull(context.UserInfo.FirstOrDefault(u => u.UserName == "TestUser"));
        }
    }
}
