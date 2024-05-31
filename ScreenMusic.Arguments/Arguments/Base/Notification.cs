﻿namespace ScreenMusic.Arguments;

public class Notification(string message, EnumMessageType messageType)
{
    public string Message { get; set; } = message;
    public EnumMessageType MessageType { get; set; } = messageType;
}