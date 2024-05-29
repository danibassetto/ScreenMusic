using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputCreateUser(string username, string password)
{
    [MaxLength(50)] public string Username { get; private set; } = username;
    [MaxLength(100)] public string Password { get; private set; } = password;
}