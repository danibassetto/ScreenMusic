using AutoMapper;
using ScreenMusic.Arguments;
using ScreenMusic.Domain.Entities;

namespace ScreenMusic.Domain.Mapping;

public class MapperEntityOutput : Profile
{
    public MapperEntityOutput()
    {
        #region Artist
        CreateMap<Artist, OutputArtist>().ReverseMap();
        #endregion

        #region Music
        CreateMap<Music, OutputMusic>().ReverseMap();
        #endregion

        #region MusicGenre
        CreateMap<MusicGenre, OutputMusicGenre>().ReverseMap();
        #endregion
    }
}