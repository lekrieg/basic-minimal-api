using AutoMapper;
using MinimalAPI.Data.Entities;
using MinimalAPI.Dtos;

namespace MinimalAPI.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoDTO>().ReverseMap();
    }
}
