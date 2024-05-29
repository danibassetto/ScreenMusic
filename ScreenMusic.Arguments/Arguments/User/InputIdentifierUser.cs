using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputIdentifierUser(string username)
{
    [MaxLength(50)] public string Username { get; private set; } = username;
}