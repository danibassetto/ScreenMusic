namespace ScreenMusic.Arguments;

public class OutputUser
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ChangeDate { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public DateTime? TokenExpirationDate { get; set; }
}