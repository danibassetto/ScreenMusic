﻿namespace ScreenMusic.Arguments;

public class InputIdentifierUser(string username)
{
    public string Username { get; private set; } = username;
}