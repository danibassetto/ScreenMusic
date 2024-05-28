using AutoMapper;

namespace ScreenMusic.Domain.Mapping;

public class MapperGeneric<TInput, TOutput> : Profile
{
    public MapperGeneric()
    {
        CreateMap<TInput, TOutput>().ReverseMap();
    }
}