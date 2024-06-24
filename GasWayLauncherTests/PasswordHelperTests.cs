using Xunit;
using GasWayLauncher.Classes;

public class PasswordHelperTests
{
    [Fact]
    public void HashPassword_ShouldReturnCorrectHash()
    {
        // Arrange
        string password = "password123";
        string expectedHash = "ef92b778bafee29a8db939e69d62ae4bfa7cf3eb0cb65e4a39e4bb9c18f7896e"; // Пример SHA256 хеша

        // Act
        string hash = PasswordHelper.HashPassword(password);

        // Assert
        Assert.Equal(expectedHash, hash);
    }
}
