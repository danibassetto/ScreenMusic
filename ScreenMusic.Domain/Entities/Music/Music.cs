namespace ScreenMusic.Domain.Entities;

public class Music : BaseEntity<Music>
{
    public string? Name { get; set; }
    public int? ReleaseYear { get; set; }
    public long? ArtistaId { get; set; }  
        
    public virtual Artist? Artist { get; set; }
    public virtual ICollection<MusicGenre>? ListMusicGenre { get; set; }

    public Music() { }

    public Music(string? name, int? releaseYear, int? artistaId)
    {
        Name = name;
        ReleaseYear = releaseYear;
        ArtistaId = artistaId;
    }

    public override string ToString()
    {
        return @$"Id: {Id}
        Nome: {Name}";
    }
}