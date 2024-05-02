using AutoMapper;
using Books.BL.Dto;
using Common.Domain;

namespace Books.BL.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UpdateBookDto, Book>();
        CreateMap<CreateBookDto, Book>();
    }
}