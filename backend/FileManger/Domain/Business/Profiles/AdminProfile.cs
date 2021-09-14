using AutoMapper;
using CrossCutting.ApiModel;
using Domain.Models;

namespace Domain.Business.Profiles
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            CreateMap<Person, PersonAM>().ReverseMap();
            CreateMap<Permissions, PermissionsAM>().ReverseMap();
            CreateMap<Structure, StructureAM>().ReverseMap();
            CreateMap<States, StatesAM>().ReverseMap();
        }
    }
}
