using AutoMapper;
using MWEntities;

namespace minesweeper_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Board, PersistibleBoard>()
                 .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Owner.Username))
                 .ForMember(dest => dest.BoardId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.BoardName, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.BoardDefinition, opt => opt.MapFrom(src => Newtonsoft.Json.JsonConvert.SerializeObject(src.Name)));


            // Add as many of these lines as you need to map your objects
            CreateMap<PersistibleBoard, Board>()
                 .ForMember(dest => dest, opt => opt.MapFrom(src => Newtonsoft.Json.JsonConvert.DeserializeObject(src.BoardDefinition)));


        }
    }

}
