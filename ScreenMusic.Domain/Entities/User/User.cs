namespace ScreenMusic.Domain.Entities;

public class User : BaseEntity<User>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
    public DateTime? TokenExpirationDate { get; set; }

    public User() { }

    public User(string username, string password, DateTime? tokenExpirationDate)
    {
        Username = username;
        Password = password;
        TokenExpirationDate = tokenExpirationDate;
    }
}