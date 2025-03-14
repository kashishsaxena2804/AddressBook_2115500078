using AutoMapper;
using ModelLayer.DTO;
using ModelLayer.Models;

namespace BusinessLayer.Mappings
{
    public class AddressBookMappingProfile : Profile
    {
        public AddressBookMappingProfile()
        {
            CreateMap<AddressBookEntry, AddressBookDTO>();
            CreateMap<AddressBookDTO, AddressBookEntry>();
        }
    }
}
