namespace ScreenMusic.Arguments;

public class InputUpdateUser(string username, string password, DateTime? tokenExpirationDate)
{
    public string Username { get; private set; } = username;
    public string Password { get; private set; } = password;
    public DateTime? TokenExpirationDate { get; private set; } = tokenExpirationDate;
}