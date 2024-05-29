namespace ScreenMusic.Domain.Entities;

public class Artist : BaseEntity<Artist>
{
    public string? Name { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? Biography { get; set; }
    public virtual ICollection<Music>? ListMusic { get; set; } = new List<Music>();

    public Artist() { }

    public Artist(string name, string profilePhoto, string biography)
    {
        Name = name;
        ProfilePhoto = profilePhoto;
        Biography = biography;
    }

    public override string ToString()
    {
        return $@"Id: {Id}
            Nome: {Name}
            Foto de Perfil: {ProfilePhoto}
            Bio: {Biography}";
    }
}