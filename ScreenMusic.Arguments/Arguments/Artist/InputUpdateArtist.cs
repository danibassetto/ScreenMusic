﻿using System.ComponentModel.DataAnnotations;

namespace ScreenMusic.Arguments;

public class InputUpdateArtist
{
    public string? ProfilePhoto { get; set; }
    [Required][MaxLength(500, ErrorMessage = "Quantidade de caracteres inválida")] public string? Biography { get; set; }

    public InputUpdateArtist() { }

    public InputUpdateArtist(string? profilePhoto, string biography)
    {
        ProfilePhoto = profilePhoto;
        Biography = biography;
    }
}