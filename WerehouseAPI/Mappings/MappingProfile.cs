using AutoMapper;
using WerehouseAPI.Dtos;
using WerehouseAPI.Entities;

namespace WerehouseAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateReceiverDto, Receiver>();
            CreateMap<CreateOrderDto, Package>();
            CreateMap<Package, GetPackageDto>().ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name));
            CreateMap<Sender, GetSenderDto>();
            CreateMap<Receiver, GetSenderDto>();
        }
    }
}
