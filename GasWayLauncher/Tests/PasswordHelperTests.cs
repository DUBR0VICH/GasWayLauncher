using Xunit;
using GasWayLauncher.Classes;

public class PasswordHelperTests
{
    [Fact]
    public void HashPassword_ShouldReturnCorrectHash()
    {
        // Arrange
        string password = "password123";
        string expectedHash = PasswordHelper.HashPassword(password);

        // Act
        string hash = PasswordHelper.HashPassword(password);

        // Assert
        Assert.Equal(expectedHash, hash);
    }
}
