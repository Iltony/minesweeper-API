using AutoMapper;
using MWEntities;
using Newtonsoft.Json;

namespace minesweeper_API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //// Add as many of these lines as you need to map your objects
            //CreateMap<Board, PersistibleBoard>()
            //     .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Owner.Username))
            //     .ForMember(dest => dest.BoardId, opt => opt.MapFrom(src => src.Id))
            //     .ForMember(dest => dest.BoardName, opt => opt.MapFrom(src => src.Name))
            //     .ForMember(dest => dest.BoardDefinition, opt => opt.MapFrom(src => Newtonsoft.Json.JsonConvert.SerializeObject(src.Name)));

            CreateMap<Board, Board>()
                .IncludeMembers(dest => dest);

            CreateMap<string, Board>()
                .IncludeMembers(src => JsonConvert.DeserializeObject<Board>(src));

            CreateMap<Board, string>()
                .IncludeMembers(src => JsonConvert.SerializeObject(src).ToString());

            CreateMap<PersistibleBoard, Board>()
                .IncludeMembers(src => src.BoardDefinition);

            // Add as many of these lines as you need to map your objects
            CreateMap<Board, PersistibleBoard>()
                 .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Owner.Username))
                 .ForMember(dest => dest.BoardId, opt => opt.MapFrom(src => src.Id))
                 .ForMember(dest => dest.BoardName, opt => opt.MapFrom(src => src.Name))
                 .ForMember(dest => dest.BoardDefinition, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src)));

        }
    }

}
