using AutoMapper;
using PruebaTecnica.Application.DTO;
using PruebaTecnica.Domain.Entities;

namespace PruebaTecnica.Application.Mappings
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UserRegisterDTO, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) 
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore()) 
            .ForMember(dest => dest.UltimoAcceso, opt => opt.MapFrom(src => DateTime.UtcNow));
        }
    }
}
