using AdminApp.Core.DTO.Item;
using AdminApp.Core.DTO.User;
using AdminApp.Core.Entities;
using AutoMapper;

namespace AdminApp.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, ItemReadDTO>();
            CreateMap<ItemAddDTO, Item>();
            CreateMap<ItemUpdateDTO, Item>();

            CreateMap<User, UserReadDTO>();
            CreateMap<UserAddDTO, User>();
            CreateMap<UserUpdateDTO, User>();
        }
    }
}
