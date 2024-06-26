﻿namespace ScreenMusic.Domain.Entities;

public class MusicGenre : BaseEntity<MusicGenre>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Music>? ListMusic { get; set; } = new List<Music>();

    public MusicGenre() { }

    public MusicGenre(string name, string description)
    {
        Name = name;
        Description = description;
    }
}