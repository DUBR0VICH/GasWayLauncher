using Xunit;
using Moq;
using System.Linq;
using GasWayLauncher.Classes;
using GasWayLauncher.Model;
using GasWayLauncher.View;
using Microsoft.EntityFrameworkCore;

public class RegisterFormTests
{
    private DbContextOptions<ContextBD> CreateNewContextOptions()
    {
        var builder = new DbContextOptionsBuilder<ContextBD>();
        builder.UseInMemoryDatabase("TestDatabase");
        return builder.Options;
    }

    [Fact]
    public void Button_Click_ShouldRegisterUser()
    {
        // Arrange
        var options = CreateNewContextOptions();

        var registerForm = new RegisterForm();
        registerForm.tb1.Text = "NewUser";
        registerForm.tb2.Password = "newpassword123";

        // Act
        registerForm.Button_Click(null, null);

        using (var context = new ContextBD(options))
        {
            // Assert
            Assert.Equal(1, context.UserInfo.Count());
            Assert.Equal("NewUser", context.UserInfo.First().UserName);
        }
    }
}
