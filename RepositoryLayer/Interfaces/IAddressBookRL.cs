using ModelLayer.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IAddressBookRL
    {
        Task<List<AddressBookDTO>> GetAllContacts();
        Task<AddressBookDTO> GetContactById(int id);
        Task<AddressBookDTO> AddContact(AddressBookDTO contact);
        Task<AddressBookDTO> UpdateContact(int id, AddressBookDTO contact);
        Task<bool> DeleteContact(int id);
    }
}
