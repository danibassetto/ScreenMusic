using AutoMapper;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Domain.Mapping;

public class MapperInputEntity : Profile
{
    public MapperInputEntity()
    {
        #region Artist
        CreateMap<InputCreateArtist, Artist>().ReverseMap();
        CreateMap<InputUpdateArtist, Artist>().ReverseMap();
        #endregion

        #region Music
        CreateMap<InputCreateMusic, Music>().ReverseMap();
        CreateMap<InputUpdateMusic, Music>().ReverseMap();
        #endregion

        #region MusicGenre
        CreateMap<InputCreateMusicGenre, MusicGenre>().ReverseMap();
        CreateMap<InputUpdateMusicGenre, MusicGenre>().ReverseMap();
        #endregion
    }
}